using Customer.Register.Application.CommandHandlers.Customers;
using Customer.Register.Application.Commands.Customers;
using Customer.Register.Domain.Repositories;
using Customer.Register.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.UnitTest.CommandHandlers.Customers
{
    [TestClass]
    public class AddCustomerUnitTest
    {
        [TestMethod]
        public void Handler_NewCustomerNoAddress_CustomerInsertedInDatabase()
        {
            Mock<ICustomerRepository> mockRep = new Mock<ICustomerRepository>();
            AddCustomerCommand command = new AddCustomerCommand()
            {
                CustomerIdentity = 1,
                Gender = 'M',
                LastName = "Calisto",
                MiddleName = "Cesar",
                Name = "Caio"
            };
            AddCustomerCommandHandler handler = new AddCustomerCommandHandler(mockRep.Object);
            Task t = Task.Run(() => handler.Handle(command, CancellationToken.None));

            mockRep.Verify(m => m.Insert(It.IsAny<Domain.Aggregate.Customer>()), Times.Once());
        }
    }
}
