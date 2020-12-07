// <copyright file="Hooks.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using BoDi;
using Contoso.Registration.FunctionalTest.Configurations;
using Contoso.Registration.FunctionalTest.Services;
using Contoso.Registration.FunctionalTest.Stubs;
using Contoso.Registration.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace Contoso.Registration.FunctionalTest.Hooks
{
    [Binding]
    internal sealed class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTests()
        {
            IConfiguration config = GetConfigurations();
            SetTableStorageConfig(config);
            ExternalServices.MessageBus = new MessageBusStub();
            // Todo: Clean database / run storage emulator locally
        }

        [AfterTestRun]
        public static void AfterTests()
        {
            // Todo: RunProcess("StopStorageEmulator.cmd");
        }

        [AfterScenario]
        public static void AfterScenarioAttribute()
        {
            ((MessageBusStub)ExternalServices.MessageBus).Clear();
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
            TableStorageConfig.ConnectionString = config["TableStorage:ConnectionString"];
            TableStorageConfig.Table = config["TableStorage:Table"];
        }

        private static IConfiguration GetConfigurations()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
