using Customer.Register.Domain.Common;
using System.Runtime.Serialization;

namespace Customer.Register.Domain.Aggregate
{
    public class Customer : Entity, IAggregateRoot
    {
        public int CustomerId { get; private set; }

        public int CustomerIdentity { get; private set; }

        public string Name { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public char Gender { get; private set; }

        [IgnoreDataMember]
        public int? AddressId { get; private set; }

        [IgnoreDataMember]
        public Address Address { get; private set; }

        public static Customer Create(int customerIdentity, string name, string middleName, string lastName, char gender)
        {
            return new Customer()
            {
                CustomerIdentity = customerIdentity,
                Name = name,
                MiddleName = middleName,
                LastName = lastName,
                Gender = gender,
            };
        }
        
        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetMiddleName(string middleName)
        {
            this.MiddleName = middleName;
        }

        public void SetLastName(string lastName)
        {
            this.LastName = lastName;
        }

        public void SetGender(char gender)
        {
            this.Gender = gender;
        }

        public void SetAddress(Address address)
        {
            this.Address = address;
        }
    }
}
