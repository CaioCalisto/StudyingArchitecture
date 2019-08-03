using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Queries
{
    public interface IRoleQueries
    {
        Task<IEnumerable<Role>> GetRolesAsync(int offset, int next);
        Task<Role> GetRoleByRoleIdAsync(int roleId);
        Task<IEnumerable<EndUser>> GetEndUsersByRoleIdAsync(int roleId, int offset, int next);
        Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(int roleId, int offset, int next);
    }
}
