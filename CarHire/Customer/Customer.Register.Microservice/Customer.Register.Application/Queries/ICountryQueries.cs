using Customer.Register.Domain.Aggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface ICountryQueries
    {
        Task<Country> GetCountryByIdAsync(int countryId);
        Task<IEnumerable<Country>> GetCountriesAsync(int offset, int next);
        Task<IEnumerable<County>> GetCountiesAsync(int countryId, int offset, int next);
    }
}
