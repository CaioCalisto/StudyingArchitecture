using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using UserAuthorization.Api.Extensions;
using UserAuthorization.Application.Configurations;
using UserAuthorization.Application.Queries;
using UserAuthorization.Domain.Repositories;
using UserAuthorization.Infrastructure.Repositories;

namespace UserAuthorization.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IEndUserRepository, EndUserRepository>();
            services.AddTransient<ISubDomainRepository, SubDomainRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IEndUserQueries, EndUserQueries>();
            services.AddTransient<IRoleQueries, RoleQueries>();
            services.AddTransient<ISubDomainQueries, SubDomainQueries>();
            services.AddMediatR(AppDomain.CurrentDomain.Load("UserAuthorization.Application"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("UserAuthorization.Infrastructure"));
            services.Configure<DatabaseConfig>(this.Configuration);

            services.AddApiVersioning(o =>
            {
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.ApiVersionReader = new MediaTypeApiVersionReader();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new Info { Title = "Authorization Api", Version = "v1.0" });
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
            services.AddCustomDbContext(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Authorization API v1.0");
            });
        }
    }
}
