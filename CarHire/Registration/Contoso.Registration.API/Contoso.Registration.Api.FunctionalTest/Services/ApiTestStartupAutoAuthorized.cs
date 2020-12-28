// <copyright file="ApiTestStartupAutoAuthorized.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.FunctionalTest.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Contoso.Registration.FunctionalTest.Services
{
    /// <summary>
    /// Test API startup.
    /// </summary>
    internal class ApiTestStartupAutoAuthorized : ApiTestStartup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiTestStartupAutoAuthorized"/> class.
        /// </summary>
        /// <param name="env">Configuration environment.</param>
        public ApiTestStartupAutoAuthorized(IConfiguration env)
            : base(env)
        {
        }

        /// <inheritdoc/>
        protected override void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseMiddleware<AutoAuthorizeMiddleware>();
            app.UseAuthorization();
        }
    }
}
