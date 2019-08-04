using Customer.Register.Application.Models;
using System.Threading.Tasks;

namespace Customer.Register.Application.HttpClients
{
    public interface ICustomerApi
    {
        Task<PaginatedResult<Models.Customer>> GetCustomers(int offset, int next);
    }
}
