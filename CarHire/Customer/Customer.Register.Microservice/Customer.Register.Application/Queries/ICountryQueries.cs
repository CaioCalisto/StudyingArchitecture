using Customer.Register.Application.Models;
using Customer.Register.Domain.Aggregate;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface ICountryQueries
    {
        Task<Country> GetCountryByIdAsync(int countryId);
        Task<PaginatedResult<Country>> GetCountriesAsync(int offset, int next);
        Task<PaginatedResult<County>> GetCountiesAsync(int countryId, int offset, int next);
    }
}
