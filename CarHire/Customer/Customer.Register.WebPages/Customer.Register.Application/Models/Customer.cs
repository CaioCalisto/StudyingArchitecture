namespace Customer.Register.Application.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public int CustomerIdentity { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public char Gender { get; set; }
    }
}
