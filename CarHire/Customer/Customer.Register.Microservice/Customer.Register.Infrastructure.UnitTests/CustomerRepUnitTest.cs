using Customer.Register.Domain.Repositories;
using Customer.Register.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Register.Infrastructure.UnitTests
{
    public class CustomerRepUnitTest
    {
        private ICustomerRepository GetRepositoryInMemory()
        {
            DbContextOptions<CustomerDBContext> dbOptions = new DbContextOptionsBuilder<CustomerDBContext>()
                            .UseInMemoryDatabase(databaseName: "CustomerDb")
                            .Options;

            CustomerDBContext context = new CustomerDBContext(dbOptions, null);
            return new CustomerRepository(context);
        }

        [Fact]
        public void Test1()
        {
            Domain.Aggregate.Customer customer = Domain.Aggregate.Customer
                .Create(1, "Caio", "Cesar", "Calisto", 'M');
            ICustomerRepository repository = GetRepositoryInMemory();
            repository.Insert(customer);
            Task saveTask = Task.Run(() => repository.UnitOfWork.SaveEntitiesAsync());
            saveTask.Wait();

            Domain.Aggregate.Customer result = repository.SelectByCustomerIdentity(1);
            Assert.Equal("Caio", result.Name);
        }
    }
}