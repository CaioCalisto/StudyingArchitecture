using Customer.Register.Domain.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Customer.Register.Domain.Aggregate
{
    public class County : Entity
    {
        public int CountyId { get; private set; }

        public string Name { get; private set; }

        [IgnoreDataMember]
        public ICollection<Address> Addresses { get; private set; }

        [IgnoreDataMember]
        public int? CountryId { get; private set; }

        [IgnoreDataMember]
        public Country Country { get; private set; }

        public static County Create(string name)
        {
            return new County()
            {
                Name = name
            };
        }
        
        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetCountry(Country country)
        {
            this.Country = country;
        }
    }
}
