using Customer.Register.Application.Models;
using Customer.Register.Domain.Aggregate;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface ICountyQueries
    {
        Task<County> GetCountyByIdAsync(int countyId);
        Task<PaginatedResult<County>> GetCountiesAsync(int offset, int next);
        Task<PaginatedResult<Address>> GetAddressesByCounty(int countyId, int offset, int next);
    }
}
