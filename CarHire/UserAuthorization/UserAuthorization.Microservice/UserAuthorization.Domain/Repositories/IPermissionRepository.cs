using System;
using System.Collections.Generic;
using System.Text;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Repositories
{
    public interface IPermissionRepository: IRepository<Permission>
    {
        Permission Insert(Permission permission);
        Permission Update(Permission permission);
        Permission SelectByPermissionId(int permissionId);
        void Remove(Permission permission);
        RolePermission Insert(RolePermission rolePermission);
        RolePermission Update(RolePermission rolePermission);
        IEnumerable<RolePermission> SelectRolePermissionByPermissionId(int permissionId);
        IEnumerable<RolePermission> SelectRolePermissionByRoleId(int roleId);
    }
}
