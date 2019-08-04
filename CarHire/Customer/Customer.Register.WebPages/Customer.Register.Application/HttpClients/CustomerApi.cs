using Customer.Register.Application.Configurations;
using Customer.Register.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Customer.Register.Application.HttpClients
{
    public class CustomerApi : ICustomerApi
    {
        private ApiEndPoints apiEndPoints;

        public CustomerApi(IOptions<ApiEndPoints> apiEndPoints)
        {
            this.apiEndPoints = apiEndPoints.Value ?? throw new ArgumentNullException(nameof(apiEndPoints));
        }

        public Task<PaginatedResult<Models.Customer>> GetCustomers(int offset, int next)
        {
            throw new NotImplementedException();
        }
    }
}
