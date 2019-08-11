using System.Collections.Generic;
using Vehicle.Register.Domain.Common;

namespace Vehicle.Register.Domain.Repositories
{
    public interface IVehicleRepository: IRepository<Aggregates.Vehicle>
    {
        Aggregates.Vehicle SelectVehicle(int vehicleId);
        Aggregates.Vehicle Insert(Aggregates.Vehicle vehicle);
        Aggregates.Vehicle Update(Aggregates.Vehicle vehicle);
        void Delete(Domain.Aggregates.Vehicle vehicle);

        Entities.Brand SelectBrand(int brandId);
        Entities.Brand Insert(Entities.Brand brand);
        Entities.Brand Update(Entities.Brand brand);
        void Delete(Entities.Brand brand);

        Entities.VehicleType SelectVehicleType(int vehicleTypeId);
        Entities.VehicleType Insert(Entities.VehicleType vehicleType);
        Entities.VehicleType Update(Entities.VehicleType vehicleType);
        void Delete(Entities.VehicleType vehicleType);

        IEnumerable<Aggregates.Vehicle> TESTE(string name);
    }
}
