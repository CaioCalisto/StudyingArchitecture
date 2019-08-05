using Customer.Register.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Application.HttpClients
{
    public interface IAuthorizationApi
    {
        Task<SubDomain> GetSubDomain(int subDomainId);
        Task<IEnumerable<Role>> GetRolesBySubDomain(int subDomainId, int offset, int next);
        Task<IEnumerable<Permission>> GetPermissionsByRole(int roleId, int offset, int next);
    }
}
