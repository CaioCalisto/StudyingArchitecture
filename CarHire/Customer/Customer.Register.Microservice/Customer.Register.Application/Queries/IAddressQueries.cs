using Customer.Register.Domain.Aggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface IAddressQueries
    {
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<IEnumerable<Address>> GetAddressesAsync(int offset, int next);
    }
}
