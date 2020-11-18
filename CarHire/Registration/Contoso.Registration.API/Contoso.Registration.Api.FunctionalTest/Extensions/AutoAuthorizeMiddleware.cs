using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Contoso.Registration.FunctionalTest.Extensions
{
    internal class AutoAuthorizeMiddleware
    {
        private readonly string identityId = "9e3163b9-1ae6-4652-9dc6-7898ab7b7a00";
        private readonly RequestDelegate next;

        public AutoAuthorizeMiddleware(RequestDelegate rd)
        {
            this.next = rd;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var identity = new ClaimsIdentity("cookies");

            identity.AddClaim(new Claim("sub", this.identityId));
            identity.AddClaim(new Claim("unique_name", this.identityId));
            identity.AddClaim(new Claim(ClaimTypes.Name, this.identityId));

            httpContext.User.AddIdentity(identity);

            await this.next.Invoke(httpContext);
        }
    }
}
