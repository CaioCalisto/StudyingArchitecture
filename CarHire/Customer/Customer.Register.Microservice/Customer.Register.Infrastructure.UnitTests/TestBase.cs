using Customer.Register.Domain.Repositories;
using Customer.Register.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customer.Register.Infrastructure.UnitTests
{
    public class TestBase
    {
        public CustomerDBContext DbContext { get; }
        public ICustomerRepository CustomerRepository { get; }

        public TestBase()
        {
            DbContext = GetDBContext(GetDbOptions());
            CustomerRepository = GetCustomerRepository(DbContext);
        }

        private CustomerDBContext GetDBContext(DbContextOptions<CustomerDBContext> dbOptions)
        {
            return new CustomerDBContext(dbOptions, null);
        }

        private DbContextOptions<CustomerDBContext> GetDbOptions()
        {
            return new DbContextOptionsBuilder<CustomerDBContext>()
                            .UseInMemoryDatabase(databaseName: "CustomerDb")
                            .Options;
        }

        private ICustomerRepository GetCustomerRepository(CustomerDBContext context)
        {
            return new CustomerRepository(context);
        }
    }
}
