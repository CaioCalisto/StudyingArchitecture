// <copyright file="Hooks.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using Contoso.Registration.FunctionalTest.Configurations;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace Contoso.Registration.FunctionalTest.Hooks
{
    /// <summary>
    /// The hooks (event bindings) can be used to perform
    /// additional automation logic on specific events,
    /// such as before executing a scenario.
    /// </summary>
    [Binding]
    public sealed class Hooks
    {
        /// <summary>
        /// Run steps before tests start.
        /// </summary>
        [BeforeTestRun]
        public static void BeforeTests()
        {
            IConfiguration config = GetConfigurations();
            SetTableStorageConfig(config);
            SetApiConfig(config);
            // Clean database
            //RunProcess("\"C:\\Program Files(x86)\\Microsoft SDKs\\Azure\\Storage Emulator\\AzureStorageEmulator.exe\" start");
            //RunProcess("StartServices.cmd");
        }

        /// <summary>
        /// Run step after tests finish.
        /// </summary>
        [AfterTestRun]
        public static void AfterTests()
        {
            RunProcess("StopStorageEmulator.cmd");
        }

        private static void RunProcess(string cmdFile)
        {
        }

        private static void SetTableStorageConfig(IConfiguration config)
        {
            TableStorageConfig.ConnectionString = config["TableStorageConfig:ConnectionString"];
            TableStorageConfig.Table = config["TableStorageConfig:Table"];
        }

        private static void SetApiConfig(IConfiguration config)
        {
            ApiConfigurations.BaseAddress = config["ApiConfigurations:BaseAddress"];
        }

        private static IConfiguration GetConfigurations()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true);

            return builder.Build();
        }
    }
}
