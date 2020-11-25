// <copyright file="AuthorizationDelegatingHandler.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contoso.Registration.UI.Authorization
{
    /// <summary>
    /// This handler get token before http calls.
    /// </summary>
    public class AuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccesor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationDelegatingHandler"/> class.
        /// </summary>
        /// <param name="httpContextAccesor">HttpContext.</param>
        public AuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccesor)
        {
            this.httpContextAccesor = httpContextAccesor;
        }

        /// <inheritdoc/>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = this.httpContextAccesor.HttpContext
                .Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            // I can get the token here
            var token = "token";

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
