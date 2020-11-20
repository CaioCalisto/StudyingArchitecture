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

            // Todo: Clean database / run storage emulator locally
        }

        /// <summary>
        /// Run step after tests finish.
        /// </summary>
        [AfterTestRun]
        public static void AfterTests()
        {
            // Todo: RunProcess("StopStorageEmulator.cmd");
        }

        private static void RunProcess(string cmdFile)
        {
            var processInfo = new ProcessStartInfo();
            processInfo.UseShellExecute = true;

            processInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;

            processInfo.FileName = cmdFile;
            processInfo.Verb = "runas";
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            using (Process process = new Process())
            {
                process.StartInfo = processInfo;
                process.Start();
                process.WaitForExit();
            }
        }

        private static void SetTableStorageConfig(IConfiguration config)
        {
            TableStorageConfig.ConnectionString = config["TableStorageConfig:ConnectionString"];
            TableStorageConfig.Table = config["TableStorageConfig:Table"];
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
