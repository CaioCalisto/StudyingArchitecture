using Customer.Register.Domain.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Customer.Register.Domain.Aggregate
{
    public class Country : Entity
    {
        public int CountryId { get; private set; }
        public string Name { get; private set; }

        [IgnoreDataMember]
        public ICollection<County> Counties { get; private set; }

        public static Country Create(string name)
        {
            return new Country()
            {
                Name = name
            };
        }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
