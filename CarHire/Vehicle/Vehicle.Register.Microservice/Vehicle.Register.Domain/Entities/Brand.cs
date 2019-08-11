using System.Collections.Generic;
using System.Runtime.Serialization;
using Vehicle.Register.Domain.Common;

namespace Vehicle.Register.Domain.Entities
{
    public class Brand: Entity
    {
        public int BrandId { get; private set; }
        public string Name { get; private set; }

        [IgnoreDataMember]
        public ICollection<Aggregates.Vehicle> Vehicles { get; private set; }

        public Brand(string name)
        {
            Name = name;
        }

    }
}
