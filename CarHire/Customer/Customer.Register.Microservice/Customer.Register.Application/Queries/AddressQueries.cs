using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Customer.Register.Application.Configurations;
using Customer.Register.Domain.Aggregate;
using Dapper;
using Microsoft.Extensions.Options;

namespace Customer.Register.Application.Queries
{
    public class AddressQueries : IAddressQueries
    {
        private DatabaseConfig dbcConfig;

        public AddressQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            string query = $"SELECT * FROM Address Where AddressId = {addressId}";
            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Address>(query);
            }
        }

        public async Task<IEnumerable<Address>> GetAddressesAsync(int offset, int next)
        {
            string query = $"SELECT * FROM Address ORDER BY AddressId " +
                         $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Address>(query);
            }
        }
    }
}
