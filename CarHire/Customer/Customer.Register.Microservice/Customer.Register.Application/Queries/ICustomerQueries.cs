using Customer.Register.Application.Models;
using System.Threading.Tasks;

namespace Customer.Register.Application.Queries
{
    public interface ICustomerQueries
    {
        Task<Domain.Aggregate.Customer> GetCostumerByIdentityAsync(int customerIdentity);
        Task<PaginatedResult<Domain.Aggregate.Customer>> GetCostumersAsync(int offset, int next);
        Task<Domain.Aggregate.Address> GetCustomerAddressAsync(int identity);
    }
}
