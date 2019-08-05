using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Configurations;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Queries
{
    public class PermissionQueries : IPermissionQueries
    {
        private DatabaseConfig dbcConfig;

        public PermissionQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task<Permission> GetPermissionByIdAsync(int permissionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetPermissionsAsync(int offset, int next)
        {
            throw new NotImplementedException();
        }
    }
}
