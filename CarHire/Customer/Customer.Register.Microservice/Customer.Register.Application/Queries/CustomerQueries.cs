using Customer.Register.Application.Configurations;
using Customer.Register.Application.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private DatabaseConfig dbcConfig;

        public CustomerQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<PaginatedResult<Domain.Aggregate.Customer>> GetCostumersAsync(int offset, int next)
        {
            string query = $"SELECT * FROM Customer ORDER BY CustomerId " +
                $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";
            string totalQuery = "SELECT count(*) FROM Customer";
            IEnumerable<Domain.Aggregate.Customer> result = null;
            int total = 0;

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                result = await connection.QueryAsync<Domain.Aggregate.Customer>(query);
                total = await connection.QueryFirstOrDefaultAsync<int>(totalQuery);
            }

            return new PaginatedResult<Domain.Aggregate.Customer>()
            {
                Result = result,
                Total = total
            };
        }

        public async Task<Domain.Aggregate.Address> GetCustomerAddressAsync(int customerIdentity)
        {
            string query = $"SELECT a.* FROM Address a, Customer c WHERE a.AddressId = c.AddressId AND c.CustomerIdentity = {customerIdentity}";
            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstAsync<Domain.Aggregate.Address>(query);
            }
        }

        public async Task<Domain.Aggregate.Customer> GetCostumerByIdentityAsync(int customerIdentity)
        {
            string query = $"SELECT * FROM Customer WHERE CustomerIdentity = {customerIdentity}";
            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Domain.Aggregate.Customer>(query);
            }
        }
    }
}
