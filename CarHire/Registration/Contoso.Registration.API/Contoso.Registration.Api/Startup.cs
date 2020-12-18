// <copyright file="Startup.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Contoso.Registration.Api.Authorization;
using Contoso.Registration.Api.Extensions;
using Contoso.Registration.Application.Commands;
using Contoso.Registration.Application.Queries;
using Contoso.Registration.Domain.Ports;
using Contoso.Registration.Infrastructure.Configurations;
using Contoso.Registration.Infrastructure.Database;
using Contoso.Registration.Infrastructure.Messaging;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            this.ConfigureOptions(services);

            services.AddMediatR(AppDomain.CurrentDomain.Load("Contoso.Registration.Application"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Contoso.Registration.Infrastructure"));
            this.AddSwagger(services);
            this.AddAuthentication(services);
            this.AddAuthorization(services);
            services.AddControllers();

            this.AddDependencyInjection(services);

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
                options.Filters.Add(typeof(ValidationFilter));
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            .AddApplicationPart(typeof(Startup).Assembly)
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddVehicleCommand>()); ;
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
        /// Add dependencies.
        /// </summary>
        /// <param name="services">Services.</param>
        protected virtual void AddDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IVehicleRepository, VehicleContext>();
            services.AddScoped<IDatabaseQueries, VehicleContext>();
            services.AddScoped<IVehiclesQueries, VehiclesQueries>();
            services.AddScoped<IMessageBus, AzureBus>();
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

        private void ConfigureOptions(IServiceCollection services)
        {
            services.Configure<AzureMessaging>(this.Configuration.GetSection(nameof(AzureMessaging)));
            services.Configure<TableStorage>(this.Configuration.GetSection(nameof(TableStorage)));
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(Microsoft.AspNetCore.Authentication.AzureAD.UI.AzureADDefaults.JwtBearerAuthenticationScheme) // AzureADDefaults.JwtBearerAuthenticationScheme or JwtBearerDefaults.AuthenticationScheme
                .AddAzureADBearer(options => this.Configuration.Bind("AzureAd", options))
                .AddJwtBearer(opt =>
                {
                    opt.Audience = this.Configuration["AzureAd:ApiIdUri"];
                    opt.Authority = $"{this.Configuration["AzureAd:Instance"]}{this.Configuration["AzureAd:TenantId"]}";
                });
        }

        private void AddAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.CanEdit, policy => policy.Requirements.Add(new CanEditRequirement()));
                AuthorizationPolicyBuilder schemes = new AuthorizationPolicyBuilder();
                schemes.AddAuthenticationSchemes(
                    Microsoft.AspNetCore.Authentication.AzureAD.UI.AzureADDefaults.JwtBearerAuthenticationScheme,
                    JwtBearerDefaults.AuthenticationScheme);
                options.AddPolicy("Schemes", schemes.RequireAuthenticatedUser().Build());
            });
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
                        new List<string>() { "user_impersonation" }
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
                            },
                        },
                    },
                });
                c.OperationFilter<AuthenticationOperationFilter>();
            });
        }
    }
}
