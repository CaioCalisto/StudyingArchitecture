using System.Collections.Generic;
using Vehicle.Register.Domain.Common;

namespace Vehicle.Register.Domain.Entities
{
    public class VehicleType : Entity
    {
        public int VehicleTypeId { get; private set; }
        public string Name { get; private set; }
        public ICollection<Aggregates.Vehicle> Vehicles { get; private set; }

        public VehicleType(int vehicleTypeId, string name)
        {
            VehicleTypeId = vehicleTypeId;
            Name = name;
        }

    }
}
