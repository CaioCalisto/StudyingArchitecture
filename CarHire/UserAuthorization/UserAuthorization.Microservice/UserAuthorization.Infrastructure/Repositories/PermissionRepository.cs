using System;
using System.Collections.Generic;
using System.Linq;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Infrastructure.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AuthorizationDBContext authorizationDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.authorizationDBContext; }
        }

        public PermissionRepository(AuthorizationDBContext authorizationDBContext)
        {
            this.authorizationDBContext = authorizationDBContext ?? throw new ArgumentNullException(nameof(authorizationDBContext));
        }

        public Permission Insert(Permission permission)
        {
            return this.authorizationDBContext
                      .Permissions
                      .Add(permission)
                      .Entity;
        }

        public Permission Update(Permission permission)
        {
            return this.authorizationDBContext
                      .Update(permission)
                      .Entity;
        }

        public Permission SelectByPermissionId(int permissionId)
        {
            return this.authorizationDBContext
                .Permissions
                .Where(p => p.PermissionId == permissionId)
                .FirstOrDefault();
        }

        public void Remove(Permission permission)
        {
            this.authorizationDBContext
                .Permissions
                .Remove(permission);
        }

        public RolePermission Insert(RolePermission rolePermission)
        {
            return this.authorizationDBContext
                .RolePermissions
                .Add(rolePermission)
                .Entity;
        }

        public RolePermission Update(RolePermission rolePermission)
        {
            return this.authorizationDBContext
                .RolePermissions
                .Update(rolePermission)
                .Entity;
        }

        public IEnumerable<RolePermission> SelectRolePermissionByPermissionId(int permissionId)
        {
            return this.authorizationDBContext
                .RolePermissions
                .Where(r => r.PermissionId == permissionId);
        }

        public IEnumerable<RolePermission> SelectRolePermissionByRoleId(int roleId)
        {
            return this.authorizationDBContext
                   .RolePermissions
                   .Where(r => r.RoleId == roleId);
        }
    }
}
