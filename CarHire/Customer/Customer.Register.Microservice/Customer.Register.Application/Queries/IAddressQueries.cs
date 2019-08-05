using Customer.Register.Application.Models;
using Customer.Register.Domain.Aggregate;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface IAddressQueries
    {
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<PaginatedResult<Address>> GetAddressesAsync(int offset, int next);
    }
}
