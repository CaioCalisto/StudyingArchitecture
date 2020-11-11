// <copyright file="DatabaseSteps.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Contoso.Registration.FunctionalTest.Configurations;
using Contoso.Registration.FunctionalTest.Model.Storage;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Contoso.Registration.FunctionalTest.Steps
{
    /// <summary>
    /// Database steps.
    /// </summary>
    [Binding]
    public class DatabaseSteps
    {
        private readonly CloudTable table;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSteps"/> class.
        /// </summary>
        public DatabaseSteps()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(TableStorageConfig.ConnectionString);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            this.table = tableClient.GetTableReference(TableStorageConfig.Table);
            this.table.CreateIfNotExists();
        }

        /// <summary>
        /// Then the vehicle is in the database.
        /// </summary>
        /// <param name="table">Parameters.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Then("the vehicle is in the database")]
        public async Task ThenTheVehicleIsInDatabase(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                string partitionKey = row["Brand"];
                string rowKey = $"{row["Brand"]} {row["Name"]} {row["Category"]}";
                TableResult result = await this.table.ExecuteAsync(TableOperation.Retrieve<TableEntityAdapter<Vehicle>>(partitionKey, rowKey));
                if (result.HttpStatusCode == 200)
                {
                    Vehicle resultFound = ((TableEntityAdapter<Vehicle>)result.Result).OriginalEntity;
                    Assert.AreEqual(row["Name"], resultFound.Name);
                    Assert.AreEqual(row["Brand"], resultFound.Brand);
                    Assert.AreEqual(row["Category"], resultFound.Category);
                    Assert.AreEqual(Convert.ToInt16(row["Doors"]), resultFound.Doors);
                    Assert.AreEqual(Convert.ToInt16(row["Passengers"]), resultFound.Passengers);
                    Assert.AreEqual(row["Transmission"], resultFound.Transmission);
                    Assert.AreEqual(Convert.ToInt16(row["Consume"]), resultFound.Consume);
                    Assert.AreEqual(Convert.ToInt16(row["Emission"]), resultFound.Emission);
                }
                else
                {
                    Assert.Fail("Data not found in Database");
                }
            }
        }
    }
}
