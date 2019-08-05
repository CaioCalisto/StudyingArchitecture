using Customer.Register.Application.Configurations;
using Customer.Register.Application.Models;
using Customer.Register.Domain.Aggregate;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public class CountyQueries : ICountyQueries
    {
        private DatabaseConfig dbcConfig;

        public CountyQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<PaginatedResult<Address>> GetAddressesByCounty(int countyId, int offset, int next)
        {
            string query = $"SELECT a.* FROM Address a, County c " +
                   $"WHERE c.CountyId = {countyId} " +
                   $"AND a.CountyId = c.CountyId " +
                   $"ORDER BY a.AddressId " +
                   $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";
            string totalQuery = $"SELECT count(*) FROM Address a, County c " +
                   $"WHERE c.CountyId = {countyId} " +
                   $"AND a.CountyId = c.CountyId " +
                   $"ORDER BY a.AddressId";
            IEnumerable<Address> result = null;
            int total = 0;

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                result = await connection.QueryAsync<Address>(query);
                total = await connection.QueryFirstOrDefaultAsync<int>(totalQuery);
            }

            return new PaginatedResult<Address>()
            {
                Result = result,
                Total = total
            };
        }

        public async Task<PaginatedResult<County>> GetCountiesAsync(int offset, int next)
        {
            string query = $"SELECT * FROM County ORDER BY CountyId " +
                         $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";
            string totalQuery = "SELECT count(*) FROM County";
            IEnumerable<County> result = null;
            int total = 0;

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                result = await connection.QueryAsync<County>(query);
                total = await connection.QueryFirstOrDefaultAsync<int>(totalQuery);
            }

            return new PaginatedResult<County>()
            {
                Result = result,
                Total = total
            };
        }

        public async Task<County> GetCountyByIdAsync(int countyId)
        {
            string query = $"SELECT * FROM County Where CountyId = {countyId}";
            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<County>(query);
            }
        }
    }
}
