// <copyright file="AuthenticationOperationFilter.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Contoso.Registration.Api.Extensions
{
    /// <summary>
    /// Authentication Operation.
    /// </summary>
    public class AuthenticationOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Apply configuration in authentication operation.
        /// </summary>
        /// <param name="operation">Operation.</param>
        /// <param name="context">Context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var oAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [oAuthScheme] = new[] { "oauth2" },
                },
            };
        }
    }
}
