// <copyright file="Startup.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using AutoMapper;
using Contoso.Registration.Api.Extensions;
using Contoso.Registration.Application.Queries;
using Contoso.Registration.Domain.Ports;
using Contoso.Registration.Infrastructure.Configurations;
using Contoso.Registration.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Contoso.Registration.Api
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TableStorageConfig>(this.Configuration.GetSection(nameof(TableStorageConfig)));
            services.AddControllers();
            services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            this.AddAutoMapper(services);

            services.AddMediatR(AppDomain.CurrentDomain.Load("Contoso.Registration.Application"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Contoso.Registration.Infrastructure"));
            this.AddSwagger(services);
            this.AddAuthentication(services);
            this.AddDependencyInjection(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App.</param>
        /// <param name="env">Environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            this.ConfigureAuth(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId(this.Configuration["AzureAD:ClientId"]);
                c.OAuthClientSecret(this.Configuration["Swagger:ClientSecret"]);
                c.OAuthRealm(this.Configuration["AzureAD:ClientId"]);
                c.OAuthAppName("Registration API V1");
                c.OAuthScopeSeparator(" ");
                c.DisplayRequestDuration();
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>() { { "resource", this.Configuration["AzureAD:ClientId"] } });
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Registration API V1");
            });
        }

        /// <summary>
        /// Configure authentication pipeline.
        /// </summary>
        /// <param name="app">ApplicationBuilder.</param>
        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        private void AddAutoMapper(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Application.Mappers.MapProfile());
                mc.AddProfile(new Infrastructure.Mappers.MapProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleContext>();
            services.AddScoped<IDatabaseQueries, VehicleContext>();
            services.AddScoped<IVehiclesQueries, VehiclesQueries>();
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.JwtBearerAuthenticationScheme)
                .AddAzureADBearer(options => this.Configuration.Bind("AzureAd", options));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Registration Api",
                    Version = "v1",
                    Description = "That is a test. Used for study.",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "OAuth2",
                            Type = SecuritySchemeType.OAuth2,
                            In = ParameterLocation.Header,
                        },
                        new List<string>() { "user_impersonation", "User.Read" }
                    },
                });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{this.Configuration["AzureAD:Instance"]}/{this.Configuration["AzureAD:TenantId"]}/oauth2/authorize"),
                            TokenUrl = new Uri($"{this.Configuration["AzureAD:Instance"]}/{this.Configuration["AzureAD:TenantId"]}/oauth2/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "user_impersonation", "Access API" },
                                { "User.Read", "Microsoft Graph" },
                            },
                        },
                    },
                });
                c.OperationFilter<AuthenticationOperationFilter>();
            });
        }
    }
}
