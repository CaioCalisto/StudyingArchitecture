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
    internal class DatabaseSteps
    {
        private static CloudTable cloudTable;

        [BeforeScenario]
        public static async Task ExecuteBeforeScenario()
        {
            SetCloudTable();
            await DeleteAllDataAsync();
        }

        [Then("the vehicle is in the database")]
        public async Task ThenTheVehicleIsInDatabase(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                string partitionKey = row["Brand"];
                string rowKey = $"{row["Brand"]} {row["Name"]} {row["Category"]}";
                TableResult result = await cloudTable.ExecuteAsync(TableOperation.Retrieve<TableEntityAdapter<Vehicle>>(partitionKey, rowKey));
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

        private static void SetCloudTable()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(TableStorageConfig.ConnectionString);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            cloudTable = tableClient.GetTableReference(TableStorageConfig.Table);
            cloudTable.CreateIfNotExists();
        }

        private static async Task DeleteAllDataAsync()
        {
            TableQuerySegment<TableEntityAdapter<Vehicle>> result = await cloudTable.ExecuteQuerySegmentedAsync(new TableQuery<TableEntityAdapter<Vehicle>>(), null);
            foreach (var row in result)
            {
                await cloudTable.ExecuteAsync(TableOperation.Delete(row));
            }
        }
    }
}
