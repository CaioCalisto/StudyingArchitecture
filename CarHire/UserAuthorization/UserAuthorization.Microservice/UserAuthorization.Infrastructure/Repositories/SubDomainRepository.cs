using System;
using System.Linq;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Infrastructure.Repositories
{
    public class SubDomainRepository : ISubDomainRepository
    {
        private readonly AuthorizationDBContext authorizationDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.authorizationDBContext; }
        }

        public SubDomainRepository(AuthorizationDBContext authorizationDBContext)
        {
            this.authorizationDBContext = authorizationDBContext ?? throw new ArgumentNullException(nameof(authorizationDBContext));
        }

        public SubDomain Insert(SubDomain subDomain)
        {
            return this.authorizationDBContext
                   .SubDomains
                   .Add(subDomain)
                   .Entity;
        }

        public SubDomain SelectById(int subDomainId)
        {
            return this.authorizationDBContext
                   .SubDomains
                   .Where(e => e.SubDomainId == subDomainId)
                   .FirstOrDefault();
        }

        public SubDomain Update(SubDomain subDomain)
        {
            return this.authorizationDBContext
                   .Update(subDomain)
                   .Entity;
        }

        public void Delete(SubDomain subDomain)
        {
            this.authorizationDBContext
                .SubDomains
                .Remove(subDomain);
        }
    }
}
