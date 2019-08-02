using MediatR;

namespace Customer.Register.Application.Commands.Customers
{
    public class UpdateCustomerCommand: IRequest<Domain.Aggregate.Customer>
    {
        public int Identity { get; private set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public int AddressId { get; set; }

        public void SetIdentity(int identity)
        {
            this.Identity = identity;
        }
    }
}
