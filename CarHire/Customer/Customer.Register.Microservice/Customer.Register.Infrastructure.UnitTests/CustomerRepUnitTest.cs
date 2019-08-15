using Customer.Register.Domain.Repositories;
using Customer.Register.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Customer.Register.Infrastructure.UnitTests
{
    public class CustomerRepUnitTest: TestBase
    {
        [Fact]
        public void Test1()
        {
            Domain.Aggregate.Customer customer = Domain.Aggregate.Customer
                .Create(1, "Caio", "Cesar", "Calisto", 'M');
            CustomerRepository.Insert(customer);
            Task saveTask = Task.Run(() => CustomerRepository.UnitOfWork.SaveEntitiesAsync());
            saveTask.Wait();

            Domain.Aggregate.Customer result = DbContext.Customers
                .Where(c => c.Name == "Caio"
                && c.MiddleName == "Cesar"
                && c.LastName == "Calisto"
                && c.Gender == 'M')
                .FirstOrDefault();
            Assert.NotNull(result);
        }
    }
}