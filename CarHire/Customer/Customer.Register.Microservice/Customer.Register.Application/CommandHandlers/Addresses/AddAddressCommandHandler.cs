using Customer.Register.Application.Commands.Addresses;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Addresses
{
    public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, Address>
    {
        private readonly ICustomerRepository customerRepository;

        public AddAddressCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Address> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            Address address = this.customerRepository.Insert(Address.Create(request.Street, request.ZipCode));
            County county = this.customerRepository.SelectCountyById(request.CountyId);
            if (county != null)
            {
                address.SetCounty(county);
            }

            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();
            return address;
        }
    }
}
