using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Configurations;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Queries
{
    public class EndUserQueries: IEndUserQueries
    {
        private DatabaseConfig dbcConfig;

        public EndUserQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task<EndUser> GetEndUserByUserNameAsync(string useName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EndUser>> GetEndUsersAsync(int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync(int endUserId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync(int endUserId, int subDomainId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesIdAsync(int endUserId, int offset, int next)
        {
            throw new NotImplementedException();
        }
    }
}
