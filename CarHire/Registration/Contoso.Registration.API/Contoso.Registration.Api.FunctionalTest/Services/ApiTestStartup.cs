// <copyright file="ApiTestStartup.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.Api;
using Contoso.Registration.Application.Queries;
using Contoso.Registration.Domain.Ports;
using Contoso.Registration.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contoso.Registration.FunctionalTest.Services
{
    internal class ApiTestStartup : Startup
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
        protected override void AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleContext>();
            services.AddScoped<IDatabaseQueries, VehicleContext>();
            services.AddScoped<IVehiclesQueries, VehiclesQueries>();
            services.AddScoped(m => ExternalServices.MessageBus);
        }
    }
}
