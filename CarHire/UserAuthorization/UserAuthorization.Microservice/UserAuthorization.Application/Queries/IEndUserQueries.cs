using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Queries
{
    public interface IEndUserQueries
    {
        Task<IEnumerable<EndUser>> GetEndUsersAsync(int offset, int next);
        Task<EndUser> GetEndUserByUserNameAsync(string useName);
        Task<IEnumerable<Role>> GetRolesIdAsync(int endUserId, int offset, int next);
        Task<IEnumerable<Permission>> GetPermissionsAsync(int endUserId, int offset, int next);
        Task<IEnumerable<Permission>> GetPermissionsAsync(int endUserId, int subDomainId, int offset, int next);
    }
}
