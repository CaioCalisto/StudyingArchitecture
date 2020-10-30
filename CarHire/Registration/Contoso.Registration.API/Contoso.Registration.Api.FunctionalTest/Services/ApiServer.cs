// <copyright file="ApiServer.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        /// <returns>Server.</returns>
        public TestServer CreateServer()
        {
            string path = Assembly.GetAssembly(typeof(ApiServer)).Location;
            IWebHostBuilder hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                }).UseStartup<ApiTestStartup>();

            return new TestServer(hostBuilder);
        }
    }
}
