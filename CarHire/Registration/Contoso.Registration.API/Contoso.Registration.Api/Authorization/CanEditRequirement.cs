// <copyright file="CanEditRequirement.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Contoso.Registration.Api.Authorization
{
    /// <summary>
    /// Requirement to check if the user can edit data.
    /// </summary>
    public class CanEditRequirement : AuthorizationHandler<CanEditRequirement>, IAuthorizationRequirement
    {
        /// <summary>
        /// Check requirement.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="requirement">Requirement.</param>
        /// <returns>Result.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditRequirement requirement)
        {
            if (context.User.IsInRole("Manager"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
