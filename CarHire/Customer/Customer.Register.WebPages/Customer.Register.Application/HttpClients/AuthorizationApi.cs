using Customer.Register.Application.Configurations;
using Customer.Register.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Application.HttpClients
{
    public class AuthorizationApi : IAuthorizationApi
    {
        private ApiEndPoints apiEndPoints;

        public AuthorizationApi(IOptions<ApiEndPoints> apiEndPoints)
        {
            this.apiEndPoints = apiEndPoints.Value ?? throw new ArgumentNullException(nameof(apiEndPoints));
        }

        public Task<IEnumerable<Permission>> GetPermissions(int endUserId, int subDomainId, int offset, int next)
        {
            throw new System.NotImplementedException();
        }
    }
}
