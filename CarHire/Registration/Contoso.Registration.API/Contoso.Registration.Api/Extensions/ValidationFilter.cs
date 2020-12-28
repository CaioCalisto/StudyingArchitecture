// <copyright file="ValidationFilter.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Contoso.Registration.Api.Extensions
{
    internal class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ProblemDetails()
                {
                    Detail = string.Join(" ", context.ModelState.Values
                        .Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage).ToArray()),
                    Title = "API Error. Please see the details.",
                    Status = StatusCodes.Status400BadRequest,
                });
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
