using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using UserAuthentication.Application.Configurations;
using UserAuthentication.Domain.Aggregates;

namespace UserAuthentication.Application.Services
{
    public class UserService : IUserService
    {
        private DatabaseConfig databaseConfig;

        public UserService(IOptions<DatabaseConfig> config)
        {
            databaseConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<User> FindUserAsync(int userId)
        {
            string query = $"SELECT * FROM EndUser WHERE UserID = {userId}";
            using (SqlConnection connection = new SqlConnection(databaseConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<User>(query);
            }
        }

        public async Task<User> FindUserAsync(string userName)
        {
            string query = $"SELECT * FROM EndUser WHERE UserName = {userName}";
            using (SqlConnection connection = new SqlConnection(databaseConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<User>(query);
            }
        }
    }
}
