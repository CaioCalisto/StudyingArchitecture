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
    public class CountryQueries : ICountryQueries
    {
        private DatabaseConfig dbcConfig;

        public CountryQueries(IOptions<DatabaseConfig> config)
        {
            dbcConfig = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<IEnumerable<County>> GetCountiesAsync(int countryId, int offset, int next)
        {
            string query = $"SELECT c1.* FROM County c1, Country c2 " +
                $"WHERE c2.CountryId = {countryId} " +
                $"AND c1.CountryId = c2.CountryId " +
                $"ORDER BY c1.CountyId " +
                $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<County>(query);
            }
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync(int offset, int next)
        {
            string query = $"SELECT * FROM Country ORDER BY CountryId " +
                      $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Country>(query);
            }
        }

        public async Task<Country> GetCountryByIdAsync(int countryId)
        {
            string query = $"SELECT * FROM Country Where CountryId = {countryId}";
            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Country>(query);
            }
        }
    }
}
