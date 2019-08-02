using Customer.Register.Domain.Aggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface ICountyQueries
    {
        Task<County> GetCountyByIdAsync(int countyId);
        Task<IEnumerable<County>> GetCountiesAsync(int offset, int next);
        Task<IEnumerable<Address>> GetAddressesByCounty(int countyId, int offset, int next);
    }
}
