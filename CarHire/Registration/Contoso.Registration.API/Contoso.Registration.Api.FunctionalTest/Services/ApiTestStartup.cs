// <copyright file="ApiTestStartup.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.Api;
using Contoso.Registration.FunctionalTest.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Contoso.Registration.FunctionalTest.Services
{
    /// <summary>
    /// Test API startup.
    /// </summary>
    public class ApiTestStartup : Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiTestStartup"/> class.
        /// </summary>
        /// <param name="env">Configuration environment.</param>
        public ApiTestStartup(IConfiguration env)
            : base(env)
        {
        }

        /// <summary>
        /// Configure Authentication for test.
        /// </summary>
        /// <param name="app">ApplicationBuilder.</param>
        protected override void ConfigureAuth(IApplicationBuilder app)
        {
            if (this.Configuration["LocalTest"] == bool.TrueString.ToLowerInvariant())
            {
                app.UseMiddleware<AutoAuthorizeMiddleware>();
                app.UseAuthorization();
            }
            else
            {
                base.ConfigureAuth(app);
            }
        }
    }
}
