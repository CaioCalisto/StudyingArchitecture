// <copyright file="ValidationFilter.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Contoso.Registration.Api.Extensions
{
    internal class ValidationFilter : ActionFilterAttribute
    {
        private readonly ILogger<ValidationFilter> logger;
        private readonly IStringLocalizer<ValidationFilter> localizer;

        public ValidationFilter(ILogger<ValidationFilter> logger, IStringLocalizer<ValidationFilter> localizer)
        {
            this.logger = logger;
            this.localizer = localizer;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string detail = string.Join(" ", context.ModelState.Values
                        .Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage).ToArray());
                context.Result = new BadRequestObjectResult(new ProblemDetails()
                {
                    Detail = detail,
                    Title = this.localizer["ValidationError_Title"],
                    Status = StatusCodes.Status400BadRequest,
                });
                this.logger.LogError(detail);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
