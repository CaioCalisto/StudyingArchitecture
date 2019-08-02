using Customer.Register.Domain.Aggregate;
using MediatR;

namespace Customer.Register.Application.Commands.Addresses
{
    public class UpdateAddressCommand: IRequest<Address>
    {
        public int AddressId { get; private set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public int CountyId { get; set; }

        public void SetAddressId(int addressId)
        {
            this.AddressId = addressId;
        }
    }
}
