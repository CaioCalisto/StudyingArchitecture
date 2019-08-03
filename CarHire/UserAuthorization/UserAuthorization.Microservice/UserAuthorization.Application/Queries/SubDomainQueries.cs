using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Configurations;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Application.Queries
{
    public class SubDomainQueries: ISubDomainQueries
    {
        private DatabaseConfig dbcConfig;

        public SubDomainQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task<IEnumerable<Role>> GetRoleBySubDomainIdAsync(int subDomainId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<SubDomain> GetSubDomainByIdAsync(int subDomainId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubDomain>> GetSubDomainsAsync(int offset, int next)
        {
            throw new NotImplementedException();
        }
    }
}
