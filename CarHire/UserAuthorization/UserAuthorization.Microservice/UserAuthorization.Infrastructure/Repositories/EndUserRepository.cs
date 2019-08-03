using System;
using System.Collections.Generic;
using System.Linq;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Infrastructure.Repositories
{
    public class EndUserRepository: IEndUserRepository
    {
        private readonly AuthorizationDBContext authorizationDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.authorizationDBContext; }
        }

        public EndUserRepository(AuthorizationDBContext authorizationDBContext)
        {
            this.authorizationDBContext = authorizationDBContext ?? throw new ArgumentNullException(nameof(authorizationDBContext));
        }

        public EndUser SelectEndUserById(int endUserId)
        {
            return this.authorizationDBContext
                .EndUsers
                .Where(e => e.EndUserId == endUserId)
                .FirstOrDefault();
        }

        public EndUser Insert(EndUser endUser)
        {
            return this.authorizationDBContext
                .EndUsers
                .Add(endUser)
                .Entity;
        }

        public EndUser Update(EndUser endUser)
        {
            return this.authorizationDBContext
                .Update(endUser)
                .Entity;
        }

        public EndUserRole Insert(EndUserRole endUserRole)
        {
            return this.authorizationDBContext
                   .EndUserRoles
                   .Add(endUserRole)
                   .Entity;
        }

        public void Remove(EndUserRole endUserRole)
        {
            this.authorizationDBContext
                .EndUserRoles
                .Remove(endUserRole);
        }

        public IEnumerable<EndUserRole> SelectEndUserRoleByRoleId(int roleId)
        {
            return this.authorizationDBContext
                .EndUserRoles
                .Where(e => e.RoleId == roleId);
        }

        public IEnumerable<EndUserRole> SelectEndUserRoleByEndUserId(int endUserId)
        {
            return this.authorizationDBContext
                   .EndUserRoles
                   .Where(e => e.EndUserId == endUserId);
        }

        public void Remove(EndUser endUser)
        {
            this.authorizationDBContext
                   .EndUsers
                   .Remove(endUser);
        }
    }
}
