using Customer.Register.Application.Configurations;
using Customer.Register.Application.Queries;
using Customer.Register.Domain.Repositories;
using Customer.Register.Infrastructure;
using Customer.Register.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace Customer.Register.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection SetupVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ApiVersionReader = new MediaTypeApiVersionReader();
            });
            return services;
        }

        public static IServiceCollection SetupConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration);
            return services;
        }

        public static IServiceCollection SetupMediatr(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.Load("Customer.Register.Application"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Customer.Register.Infrastructure"));
            return services;
        }

        public static IServiceCollection SetupDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerQueries, CustomerQueries>();
            services.AddTransient<ICountryQueries, CountryQueries>();
            services.AddTransient<ICountyQueries, CountyQueries>();
            services.AddTransient<IAddressQueries, AddressQueries>();
            return services;
        }

        public static IServiceCollection SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "Customer Register Api", Version = "v1.0" });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }
                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });
                c.OperationFilter<ApiVersionOperationFilter>();
            });
            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<CustomerDBContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped
                   );

            return services;
        }
    }
}
