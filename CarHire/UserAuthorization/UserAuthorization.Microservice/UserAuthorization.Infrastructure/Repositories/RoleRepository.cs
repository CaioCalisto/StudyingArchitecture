using System;
using System.Collections.Generic;
using System.Linq;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthorizationDBContext authorizationDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.authorizationDBContext; }
        }

        public RoleRepository(AuthorizationDBContext authorizationDBContext)
        {
            this.authorizationDBContext = authorizationDBContext ?? throw new ArgumentNullException(nameof(authorizationDBContext));
        }

        public Role Insert(Role role)
        {
            return this.authorizationDBContext
                   .Roles
                   .Add(role)
                   .Entity;
        }

        public Role Update(Role role)
        {
            return this.authorizationDBContext
                   .Update(role)
                   .Entity;
        }

        public Role SelectRoleById(int RoleId)
        {
            return this.authorizationDBContext
                   .Roles
                   .Where(r => r.RoleId == RoleId)
                   .FirstOrDefault();
        }

        public void Remove(Role role)
        {
            this.authorizationDBContext
                .Roles
                .Remove(role);
        }

        public IEnumerable<Role> SelectRolesBySubDomainId(int subDomainId)
        {
            return this.authorizationDBContext
                .Roles
                .Where(r => r.SubDomainId == subDomainId);
        }
    }
}
