using Customer.Register.Application.Commands.Customers;
using Customer.Register.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Customers
{
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Domain.Aggregate.Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Domain.Aggregate.Customer> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            Domain.Aggregate.Customer customer = this.customerRepository
                .Insert(Domain.Aggregate.Customer.Create(request.CustomerIdentity, 
                request.Name, request.MiddleName, 
                request.LastName, 
                request.Gender));
            Domain.Aggregate.Address address = this.customerRepository.SelectAddressById(request.AddressId);
            if (address != null)
            {
                customer.SetAddress(address);
            }
            
            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();
            return customer;
        }
    }
}
