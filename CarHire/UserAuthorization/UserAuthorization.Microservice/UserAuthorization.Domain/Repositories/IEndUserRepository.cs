using System.Collections.Generic;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Repositories
{
    public interface IEndUserRepository : IRepository<EndUser>
    {
        EndUser Insert(EndUser endUser);
        EndUser Update(EndUser endUser);
        EndUser SelectEndUserById(int endUserId);
        void Remove(EndUser endUser);

        EndUserRole Insert(EndUserRole endUserRole);
        void Remove(EndUserRole endUserRole);

        IEnumerable<EndUserRole> SelectEndUserRoleByRoleId(int roleId);
        IEnumerable<EndUserRole> SelectEndUserRoleByEndUserId(int endUserId);
    }
}
