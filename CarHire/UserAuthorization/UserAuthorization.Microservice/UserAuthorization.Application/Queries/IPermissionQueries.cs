using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Queries
{
    public interface IPermissionQueries
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync(int offset, int next);
        Task<Permission> GetPermissionByIdAsync(int permissionId);
    }
}
