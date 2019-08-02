using Customer.Register.Application.Commands.Customers;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Customers
{
    class AddCustomerAddressCommandHandler : IRequestHandler<AddCustomerAddressCommand, bool>
    {
        private readonly ICustomerRepository customerRepository;

        public AddCustomerAddressCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<bool> Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            Address address = this.customerRepository.SelectAddressById(request.AddressId);
            Domain.Aggregate.Customer customer = this.customerRepository.SelectByCustomerIdentity(request.CustomerIdentity);
            customer.SetAddress(address);

            this.customerRepository.Update(customer);
            this.customerRepository.Update(address);
            bool result = await this.customerRepository.UnitOfWork.SaveEntitiesAsync();

            return result;
        }
    }
}
