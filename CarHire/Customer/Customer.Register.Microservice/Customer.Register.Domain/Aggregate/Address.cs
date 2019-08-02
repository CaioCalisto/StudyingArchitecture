using Customer.Register.Domain.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Customer.Register.Domain.Aggregate
{
    public class Address : Entity
    {
        public int AddressId { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }

        [IgnoreDataMember]
        public ICollection<Customer> Customers { get; private set; }

        [IgnoreDataMember]
        public int? CountyId { get; private set; }

        [IgnoreDataMember]
        public County County { get; private set; }

        public static Address Create(string street, string zipCode)
        {
            return new Address()
            {
                Street = street,
                ZipCode = zipCode
            };
        }

        public void SetStreet(string street)
        {
            this.Street = street;
        }

        public void SetZipCode(string zipCode)
        {
            this.ZipCode = zipCode;
        }
        
        public void SetCounty(County county)
        {
            this.County = county;
        }
    }
}
