// <copyright file="ExceptionFilter.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Contoso.Registration.Api.Extensions
{
    internal class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> logger;
        private readonly IStringLocalizer<ExceptionFilter> localizer;

        public ExceptionFilter(ILogger<ExceptionFilter> logger, IStringLocalizer<ExceptionFilter> localizer)
        {
            this.logger = logger;
            this.localizer = localizer;
        }

        public void OnException(ExceptionContext context)
        {
            this.logger.LogError(context.Exception.GetBaseException(), "Error");
            context.Result = new BadRequestObjectResult(new ProblemDetails()
            {
                Detail = context.Exception.GetBaseException().Message,
                Title = this.localizer["ExceptionError_Title"],
                Status = StatusCodes.Status400BadRequest,
            });
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
