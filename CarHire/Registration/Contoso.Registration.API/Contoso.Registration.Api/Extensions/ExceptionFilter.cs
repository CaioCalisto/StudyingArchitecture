// <copyright file="ExceptionFilter.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Contoso.Registration.Api.Extensions
{
    /// <summary>
    /// Custom exception filter.
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// OnException event handler.
        /// </summary>
        /// <param name="context">Context.</param>
        public void OnException(ExceptionContext context)
        {
            this.logger.LogError(context.Exception.GetBaseException(), "Error");
            context.Result = new BadRequestObjectResult(new ProblemDetails()
            {
                Detail = context.Exception.GetBaseException().Message,
                Title = "API Error. Please see the details.",
                Status = StatusCodes.Status400BadRequest,
            });
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
