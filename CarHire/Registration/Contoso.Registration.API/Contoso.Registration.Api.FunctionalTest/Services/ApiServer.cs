// <copyright file="ApiServer.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Contoso.Registration.FunctionalTest.Services
{
    /// <summary>
    /// API Server to test.
    /// </summary>
    public class ApiServer
    {
        /// <summary>
        /// Create API Server to test.
        /// </summary>
        /// <param name="autoAuthorized">Enable or disable Auto authorize.</param>
        /// <returns>Server.</returns>
        public TestServer CreateServer(bool autoAuthorized)
        {
            string path = Assembly.GetAssembly(typeof(ApiServer)).Location;
            IWebHostBuilder hostBuilder = null;

            if (autoAuthorized)
            {
                hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<ApiTestStartupAutoAuthorized>();
            }
            else
            {
                hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<ApiTestStartup>();
            }

            return new TestServer(hostBuilder);
        }
    }
}
