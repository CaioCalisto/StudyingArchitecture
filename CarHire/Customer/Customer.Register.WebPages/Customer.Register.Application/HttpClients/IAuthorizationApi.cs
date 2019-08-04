using Customer.Register.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Application.HttpClients
{
    public interface IAuthorizationApi
    {
        Task<IEnumerable<Permission>> GetPermissions(int endUserId, int subDomainId, int offset, int next);
    }
}
