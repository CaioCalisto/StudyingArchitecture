using Customer.Register.Application.Configurations;
using Customer.Register.Application.Queries;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Customer.Register.Application.UnitTest.Queries
{
    [TestClass]
    public class CustomerQueriesUnitTest
    {
        [TestMethod]
        public void GetCostumersAsync_Test()
        {
            Mock<IOptions<DatabaseConfig>> optionsMock = new Mock<IOptions<DatabaseConfig>>();
            optionsMock.SetupGet(o => o.Value).Returns(new DatabaseConfig() { ConnectionString = "conString" });
            ICustomerQueries queries = new CustomerQueries(optionsMock.Object);
            Task queryTask = Task.Run(() => queries.GetCostumersAsync(0, 10));

        }
    }
}
