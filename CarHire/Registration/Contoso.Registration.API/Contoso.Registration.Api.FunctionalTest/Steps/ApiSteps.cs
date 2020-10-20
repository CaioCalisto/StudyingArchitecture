// <copyright file="ApiSteps.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Contoso.Registration.FunctionalTest.Configurations;
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
        private const string VehicleUri = "/api/vehicles";
        private HttpResponseMessage responseMessage;

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

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiConfigurations.BaseAddress);
                    var json = JsonConvert.SerializeObject(command);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    this.responseMessage = await client.PostAsync(VehicleUri, content);
                }
            }
        }

        /// <summary>
        /// Then the API status result is.
        /// </summary>
        /// <param name="statusCode">Status code.</param>
        [Then("the API status result is (.*)")]
        public void ThenTheAPIStatusResultIs(int statusCode)
        {
            Assert.AreEqual(statusCode, (int)this.responseMessage.StatusCode);
        }
    }
}