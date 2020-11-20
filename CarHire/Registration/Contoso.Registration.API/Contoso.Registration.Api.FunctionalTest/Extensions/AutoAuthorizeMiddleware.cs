using System.Security.Claims;
using System.Threading.Tasks;
using Contoso.Registration.Api.Authorization;
using Microsoft.AspNetCore.Http;

namespace Contoso.Registration.FunctionalTest.Extensions
{
    internal class AutoAuthorizeMiddleware
    {
        private readonly RequestDelegate next;

        public AutoAuthorizeMiddleware(RequestDelegate rd)
        {
            this.next = rd;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var identity = new ClaimsIdentity("cookies");

            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Manager));

            httpContext.User.AddIdentity(identity);

            await this.next.Invoke(httpContext);
        }
    }
}
