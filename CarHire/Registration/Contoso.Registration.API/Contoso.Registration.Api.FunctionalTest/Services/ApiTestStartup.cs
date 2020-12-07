// <copyright file="ApiTestStartup.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using BoDi;
using Contoso.Registration.Api;
using Contoso.Registration.Application.Queries;
using Contoso.Registration.Domain.Ports;
using Contoso.Registration.FunctionalTest.Extensions;
using Contoso.Registration.Infrastructure.Database;
using Contoso.Registration.Infrastructure.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        protected override void AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleContext>();
            services.AddScoped<IDatabaseQueries, VehicleContext>();
            services.AddScoped<IVehiclesQueries, VehiclesQueries>();
            services.AddScoped(m => ExternalServices.MessageBus);
        }
    }
}
