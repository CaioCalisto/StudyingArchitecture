using MediatR;

namespace Customer.Register.Application.Commands.Customers
{
    public class AddCustomerAddressCommand: IRequest<bool>
    {
        public int CustomerIdentity { get; private set; }
        public int AddressId { get; set; }
        public void SetCustomerIdentity(int identity)
        {
            this.CustomerIdentity = identity;
        }
    }
}
