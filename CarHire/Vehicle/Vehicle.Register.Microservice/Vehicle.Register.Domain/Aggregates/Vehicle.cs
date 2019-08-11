using System.Runtime.Serialization;
using Vehicle.Register.Domain.Common;
using Vehicle.Register.Domain.Entities;

namespace Vehicle.Register.Domain.Aggregates
{
    public class Vehicle: Entity, IAggregateRoot
    {
        public int VehicleId { get; private set; }
        public string Name { get; private set; }

        public Vehicle(int vehicleId, string name, VehicleType vehicleType)
        {
            VehicleId = vehicleId;
            Name = name;
            VehicleType = vehicleType;
        }

        public Vehicle(int vehicleId, string name)
            :this(vehicleId, name, null)
        {
        }

        [IgnoreDataMember]
        public int? BrandId { get; private set; }

        [IgnoreDataMember]
        public VehicleType VehicleType { get; private set; }

        [IgnoreDataMember]
        public int? VehicleTypeId { get; private set; }

        [IgnoreDataMember]
        public Brand Brand { get; private set; }

        public void SetBrand(Brand brand)
        {
            this.Brand = brand;
        }

        public void SetVehicleType(VehicleType vehicleType)
        {
            this.VehicleType = vehicleType;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
