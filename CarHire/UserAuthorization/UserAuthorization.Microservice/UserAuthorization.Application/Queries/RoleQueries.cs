using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Configurations;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Queries
{
    public class RoleQueries : IRoleQueries
    {
        private DatabaseConfig dbcConfig;

        public RoleQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task<IEnumerable<EndUser>> GetEndUsersByRoleIdAsync(int roleId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(int roleId, int offset, int next)
        {
            throw new NotImplementedException();
        }

        public Task<Role> GetRoleByRoleIdAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync(int offset, int next)
        {
            throw new NotImplementedException();
        }
    }
}
