using MediatR;

namespace Customer.Register.Application.Commands.Customers
{
    public class AddCustomerCommand: IRequest<Domain.Aggregate.Customer>
    {
        public int CustomerIdentity { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public int AddressId { get; set; }
    }
}
