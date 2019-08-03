using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Application.Queries
{
    public interface ISubDomainQueries
    {
        Task<IEnumerable<SubDomain>> GetSubDomainsAsync(int offset, int next);
        Task<SubDomain> GetSubDomainByIdAsync(int subDomainId);
        Task<IEnumerable<Role>> GetRolesBySubDomainIdAsync(int subDomainId, int offset, int next);
    }
}
