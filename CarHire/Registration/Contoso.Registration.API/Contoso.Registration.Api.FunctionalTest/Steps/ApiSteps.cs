// <copyright file="ApiSteps.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contoso.Registration.Application.Model;
using Contoso.Registration.FunctionalTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace Contoso.Registration.FunctionalTest.Steps
{
    [Binding]
    internal class APISteps
    {
        private const string VehicleUri = "api/v1/vehicles";
        private HttpResponseMessage responsePostMessage;
        private HttpResponseMessage responseGetMessage;
        private bool autoAuthorized;

        [Given("the API is running")]
        public void GivenTheAPIIsRunning()
        {
        }

        [Given("user is authenticated")]
        public void GivenUserIsAuthenticatedAndAuthorized()
        {
            this.autoAuthorized = true;
        }

        [Given("user has no permission")]
        public void GivenUserHasNoPermission()
        {
            this.autoAuthorized = false;
        }

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

                using (TestServer server = new ApiServer().CreateServer(this.autoAuthorized))
                {
                    using (HttpClient client = server.CreateClient())
                    {
                        string json = JsonConvert.SerializeObject(command);
                        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                        this.responsePostMessage = await client.PostAsync(VehicleUri, content);
                    }
                }
            }
        }

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
                query += row.ContainsKey("Page") ? string.IsNullOrEmpty(row["Page"]) ? string.Empty : $"&page={row["Page"]}" : string.Empty;
                query += row.ContainsKey("Limit") ? string.IsNullOrEmpty(row["Limit"]) ? string.Empty : $"&limit={row["Limit"]}" : string.Empty;
                query = query.Replace("?&", "?");
                query = query.Replace("&&", "&");
                query = query.Substring(query.Length - 1, 1) == "&" ? query.Substring(0, query.Length - 1) : query;

                using (TestServer server = new ApiServer().CreateServer(this.autoAuthorized))
                {
                    using (HttpClient client = server.CreateClient())
                    {
                        this.responseGetMessage = await client.GetAsync(query);
                    }
                }
            }
        }

        [Then("the API Post status result is (.*)")]
        public void ThenTheAPIPostStatusResultIs(int statusCode) => Assert.AreEqual(statusCode, (int)this.responsePostMessage.StatusCode);

        [Then("the API Get status result is (.*)")]
        public void ThenTheAPIGetStatusResultIs(int statusCode) => Assert.AreEqual(statusCode, (int)this.responseGetMessage.StatusCode);

        [Then("the API Post response has the following result")]
        public async Task TheAPIPOSTResponseHasTheFollowingResult(Table table)
        {
            if (this.responsePostMessage.IsSuccessStatusCode)
            {
                string response = await this.responsePostMessage.Content.ReadAsStringAsync();
                this.CheckApiResponse(response, table);
            }
        }

        [Then("the API GET response has the following result")]
        public async Task TheAPIGETResponseHasTheFollowingResult(Table table)
        {
            if (this.responseGetMessage.IsSuccessStatusCode)
            {
                string response = await this.responseGetMessage.Content.ReadAsStringAsync();
                this.CheckApiResponse(response, table);
            }
        }

        [Then("The API POST error response has the following result")]
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

        [Then("The API GET error response has the following result")]
        public async Task TheAPIGetErrorResponseHasTheFollowingResult(Table table)
        {
            string response = await this.responseGetMessage.Content.ReadAsStringAsync();
            ProblemDetails problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(response);
            foreach (TableRow row in table.Rows)
            {
                Assert.AreEqual(Convert.ToInt16(row["StatusCode"]), problemDetails.Status);
                Assert.AreEqual(row["Title"], problemDetails.Title);
                Assert.AreEqual(row["Detail"], problemDetails.Detail);
            }
        }

        [Then("the API GET response has the following header result")]
        public void TheAPIGETResponseHasTheFollowingHeaderResult(Table table)
        {
            foreach (var row in table.Rows)
            {
                foreach (string headerValue in this.responseGetMessage.Headers.GetValues(row["Header"]))
                {
                    Assert.AreEqual(row["Value"], JsonConvert.DeserializeObject<Dictionary<string, string>>(headerValue)[row["Key"]]);
                }
            }
        }

        [Then("the API GET response contains (.*) result")]
        public async Task TheAPIGETResponseContainsResult(int results)
        {
            string response = await this.responseGetMessage.Content.ReadAsStringAsync();
            List<Vehicle> actual = JsonConvert.DeserializeObject<List<Vehicle>>(response);
            Assert.AreEqual(1, actual.Count);
        }

        private void CheckApiResponse(string json, Table table)
        {
            List<Vehicle> actual = JsonConvert.DeserializeObject<List<Vehicle>>(json);

            foreach (TableRow row in table.Rows)
            {
                Assert.IsTrue(actual.Exists(v =>
                    (row.ContainsKey("Brand") ? actual.Exists(v => v.Brand.Equals(row["Brand"])) : true) &&
                    (row.ContainsKey("Name") ? actual.Exists(v => v.Name.Equals(row["Name"])) : true) &&
                    (row.ContainsKey("Category") ? actual.Exists(v => v.Category.Equals(row["Category"])) : true) &&
                    (row.ContainsKey("Transmission") ? actual.Exists(v => v.Transmission.Equals(row["Transmission"])) : true) &&
                    (row.ContainsKey("Doors") ? actual.Exists(v => v.Doors == Convert.ToInt32(row["Doors"])) : true) &&
                    (row.ContainsKey("Passengers") ? actual.Exists(v => v.Passengers == Convert.ToInt32(row["Passengers"])) : true) &&
                    (row.ContainsKey("Consume") ? actual.Exists(v => v.Consume == Convert.ToInt32(row["Consume"])) : true) &&
                    (row.ContainsKey("Emission") ? actual.Exists(v => v.Emission == Convert.ToInt32(row["Emission"])) : true)
                ));
            }
        }
    }
}