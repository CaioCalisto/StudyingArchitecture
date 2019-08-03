using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Application.Queries
{
    public interface IEndUserQueries
    {
        Task<IEnumerable<EndUser>> GetEndUsersAsync(int offset, int next);
        Task<EndUser> GetEndUserByUserNameAsync(string useName);
        Task<IEnumerable<Role>> GetRolesByEndUserIdAsync(int endUserId, int offset, int next);
    }
}
