using Customer.Register.Application.Commands.Addresses;
using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Application.CommandHandlers.Addresses
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Address>
    {
        private readonly ICustomerRepository customerRepository;

        public UpdateAddressCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Address> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            Address address = this.customerRepository.SelectAddressById(request.AddressId);
            County county = this.customerRepository.SelectCountyById(request.CountyId);
            address.SetStreet(request.Street);
            address.SetZipCode(request.ZipCode);
            if (county != null)
            {
                address.SetCounty(county);
            }
            this.customerRepository.Update(address);
            await this.customerRepository.UnitOfWork.SaveEntitiesAsync();

            return address;
        }
    }
}
