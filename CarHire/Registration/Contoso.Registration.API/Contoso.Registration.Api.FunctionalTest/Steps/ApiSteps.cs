// <copyright file="ApiSteps.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contoso.Registration.FunctionalTest.Model.API;
using Contoso.Registration.FunctionalTest.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Contoso.Registration.FunctionalTest.Steps
{
    /// <summary>
    /// API steps to test.
    /// </summary>
    [Binding]
    public class APISteps
    {
        private const string VehicleUri = "api/v1/vehicles";
        private HttpResponseMessage responsePostMessage;
        private HttpResponseMessage responseGetMessage;

        /// <summary>
        /// Given the API is running.
        /// </summary>
        [Given("the API is running")]
        public void GivenTheAPIIsRunning()
        {
        }

        /// <summary>
        /// When a POST call is made to add new vehicle.
        /// </summary>
        /// <param name="table">Parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [When("a POST call is made to add new vehicle")]
        public async Task WhenAPostCallIsMadeToAddNewVehicle(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                var command = new
                {
                    Name = row["Name"],
                    Brand = row["Brand"],
                    Category = row["Category"],
                    Doors = Convert.ToInt16(row["Doors"]),
                    Passengers = Convert.ToInt16(row["Passengers"]),
                    Transmission = row["Transmission"],
                    Consume = Convert.ToInt16(row["Consume"]),
                    Emission = Convert.ToInt16(row["Emission"]),
                };

                using (TestServer server = new ApiServer().CreateServer())
                {
                    using (HttpClient client = server.CreateClient())
                    {
                        string json = JsonConvert.SerializeObject(command);
                        StringContent content = new StringContent(json, UTF8Encoding.UTF8, "application/json");
                        this.responsePostMessage = await client.PostAsync(VehicleUri, content);
                    }
                }
            }
        }

        /// <summary>
        /// A GET call is made.
        /// </summary>
        /// <param name="table">Parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [When("a GET call is made with the following parameters")]
        public async Task WhenAGETCallIsMadeWithTheFollowingParameters(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                string query = $"{VehicleUri}/query?";
                query += row.ContainsKey("Name") ? string.IsNullOrEmpty(row["Name"]) ? string.Empty : $"&name={row["Name"]}&" : string.Empty;
                query += row.ContainsKey("Brand") ? string.IsNullOrEmpty(row["Brand"]) ? string.Empty : $"&brand={row["Brand"]}&" : string.Empty;
                query += row.ContainsKey("Category") ? string.IsNullOrEmpty(row["Category"]) ? string.Empty : $"&category={row["Category"]}&" : string.Empty;
                query += row.ContainsKey("Doors") ? string.IsNullOrEmpty(row["Doors"]) ? string.Empty : $"&doors={row["Doors"]}&" : string.Empty;
                query += row.ContainsKey("Passengers") ? string.IsNullOrEmpty(row["Passengers"]) ? string.Empty : $"&passengers={row["Passengers"]}&" : string.Empty;
                query += row.ContainsKey("Transmission") ? string.IsNullOrEmpty(row["Transmission"]) ? string.Empty : $"&transmission={row["Transmission"]}&" : string.Empty;
                query += row.ContainsKey("Consume") ? string.IsNullOrEmpty(row["Consume"]) ? string.Empty : $"&consume={row["Consume"]}&" : string.Empty;
                query += row.ContainsKey("Emission") ? string.IsNullOrEmpty(row["Emission"]) ? string.Empty : $"&emission={row["Emission"]}&" : string.Empty;
                query = query.Replace("?&", "?");
                query = query.Replace("&&", "&");
                query = query.Substring(query.Length - 1, 1) == "&" ? query.Substring(0, query.Length - 1) : query;

                using (TestServer server = new ApiServer().CreateServer())
                {
                    using (HttpClient client = server.CreateClient())
                    {
                        this.responseGetMessage = await client.GetAsync(query);
                    }
                }
            }
        }

        /// <summary>
        /// Then the API status result is.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        [Then("the API Post status result is (.*)")]
        public void ThenTheAPIPostStatusResultIs(int statusCode) => Assert.AreEqual(statusCode, (int)this.responsePostMessage.StatusCode);

        /// <summary>
        /// Then the API Get status result is.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        [Then("the API Get status result is (.*)")]
        public void ThenTheAPIGetStatusResultIs(int statusCode) => Assert.AreEqual(statusCode, (int)this.responseGetMessage.StatusCode);

        /// <summary>
        /// Then the API Post response content is.
        /// </summary>
        /// <param name="table">Content.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then("the API Post response has the following result")]
        public async Task TheAPIPOSTResponseHasTheFollowingResult(Table table)
        {
            if (this.responsePostMessage.IsSuccessStatusCode)
            {
                string response = await this.responsePostMessage.Content.ReadAsStringAsync();
                this.CheckApiResponse(response, table);
            }
        }

        /// <summary>
        /// Then the API GET response content is.
        /// </summary>
        /// <param name="table">Content.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then("the API GET response has the following result")]
        public async Task TheAPIGETResponseHasTheFollowingResult(Table table)
        {
            if (this.responseGetMessage.IsSuccessStatusCode)
            {
                string response = await this.responseGetMessage.Content.ReadAsStringAsync();
                this.CheckApiResponse(response, table);
            }
        }

        /// <summary>
        /// The API error response has the following result.
        /// </summary>
        /// <param name="table">Parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then("The API error response has the following result")]
        public async Task TheAPIErrorResponseHasTheFollowingResult(Table table)
        {
            string response = await this.responsePostMessage.Content.ReadAsStringAsync();
            ProblemDetails problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(response);
            foreach (TableRow row in table.Rows)
            {
                Assert.AreEqual(Convert.ToInt16(row["StatusCode"]), problemDetails.Status);
                Assert.AreEqual(row["Title"], problemDetails.Title);
                Assert.AreEqual(row["Detail"], problemDetails.Detail);
            }
        }

        private void CheckApiResponse(string json, Table table)
        {
            List<Vehicle> actual = JsonConvert.DeserializeObject<List<Vehicle>>(json);
            List<Vehicle> expected = new List<Vehicle>();
            foreach (TableRow row in table.Rows)
            {
                expected.Add(new Vehicle()
                {
                    Name = row["Name"],
                    Brand = row["Brand"],
                    Category = row["Category"],
                    Doors = Convert.ToInt16(row["Doors"]),
                    Passengers = Convert.ToInt16(row["Passengers"]),
                    Transmission = row["Transmission"],
                    Consume = Convert.ToInt16(row["Consume"]),
                    Emission = Convert.ToInt16(row["Emission"]),
                });
            }

            actual.Should().BeEquivalentTo(expected);
        }
    }
}