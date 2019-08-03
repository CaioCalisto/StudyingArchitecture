using System.Collections.Generic;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role Insert(Role role);
        Role Update(Role role);
        Role SelectRoleById(int roleId);
        void Remove(Role role);
        IEnumerable<Role> SelectRolesBySubDomainId(int subDomainId);
        Permission Insert(Permission permission);
        Permission Update(Permission permission);
        Permission SelectByPermissionId(int permissionId);
        void Remove(Permission permission);
    }
}
