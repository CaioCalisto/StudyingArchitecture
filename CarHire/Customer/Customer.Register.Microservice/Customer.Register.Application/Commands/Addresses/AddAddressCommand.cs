using Customer.Register.Domain.Aggregate;
using MediatR;

namespace Customer.Register.Application.Commands.Addresses
{
    public class AddAddressCommand: IRequest<Address>
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int CountyId { get; set; }
    }
}
