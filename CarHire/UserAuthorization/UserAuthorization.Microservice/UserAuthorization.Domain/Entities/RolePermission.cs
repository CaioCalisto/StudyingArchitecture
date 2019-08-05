using System;
using System.Collections.Generic;
using System.Text;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;

namespace UserAuthorization.Domain.Entities
{
    public class RolePermission: Entity
    {
        public int PermissionId { get; private set; }
        public Permission Permission { get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public static RolePermission Create(int permissionId, int roleId)
        {
            return new RolePermission()
            {
                PermissionId = permissionId,
                RoleId = roleId
            };
        }
    }
}
