using Customer.Register.Application.Commands.Customers;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Customers
{
    public class UpdateCostumerCommandHandler : IRequestHandler<UpdateCustomerCommand, Domain.Aggregate.Customer>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateCostumerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Domain.Aggregate.Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Address address = this.customerRepository.SelectAddressById(request.AddressId);
            Domain.Aggregate.Customer customer = this.customerRepository
                .SelectByCustomerIdentity(request.Identity);
            customer.SetName(request.Name);
            customer.SetMiddleName(request.MiddleName);
            customer.SetLastName(request.LastName);
            customer.SetGender(request.Gender);
            if (address != null)
            {
                customer.SetAddress(address);
            }

            customer = this.customerRepository.Update(customer);
            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();
            return customer;
        }
    }
}
