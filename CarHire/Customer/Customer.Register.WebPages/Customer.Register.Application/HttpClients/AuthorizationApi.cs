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

        public Task<IEnumerable<Permission>> GetPermissionsByRole(int roleId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesBySubDomain(int subDomainId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<SubDomain> GetSubDomain(int subDomainId)
        {
            throw new NotImplementedException();
        }
    }
}
