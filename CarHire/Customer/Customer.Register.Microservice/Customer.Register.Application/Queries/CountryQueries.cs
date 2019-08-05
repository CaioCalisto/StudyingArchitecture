using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Customer.Register.Application.Configurations;
using Customer.Register.Application.Models;
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

        public async Task<PaginatedResult<County>> GetCountiesAsync(int countryId, int offset, int next)
        {
            string query = $"SELECT c1.* FROM County c1, Country c2 " +
                $"WHERE c2.CountryId = {countryId} " +
                $"AND c1.CountryId = c2.CountryId " +
                $"ORDER BY c1.CountyId " +
                $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";
            string queryTotal = $"SELECT count(*) FROM County c1, Country c2 " +
                $"WHERE c2.CountryId = {countryId} " +
                $"AND c1.CountryId = c2.CountryId " +
                $"ORDER BY c1.CountyId ";
            IEnumerable<County> result = null;
            int total = 0;

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                result = await connection.QueryAsync<County>(query);
                total = await connection.QueryFirstOrDefaultAsync<int>(queryTotal);
            }

            return new PaginatedResult<County>()
            {
                Result = result,
                Total = total
            };
        }

        public async Task<PaginatedResult<Country>> GetCountriesAsync(int offset, int next)
        {
            string query = $"SELECT * FROM Country ORDER BY CountryId " +
                      $"OFFSET {offset} ROWS FETCH NEXT {next} ROWS ONLY";
            string queryTotal = "SELECT count(*) FROM Country";
            IEnumerable<Country> result = null;
            int total = 0;

            using (SqlConnection connection = new SqlConnection(dbcConfig.ConnectionString))
            {
                connection.Open();
                result = await connection.QueryAsync<Country>(query);
                total = await connection.QueryFirstOrDefaultAsync<int>(queryTotal);
            }

            return new PaginatedResult<Country>()
            {
                Result = result,
                Total = total
            };
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
