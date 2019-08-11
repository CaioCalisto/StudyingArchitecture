using System;
using System.Collections.Generic;
using System.Linq;
using Vehicle.Register.Domain.Aggregates;
using Vehicle.Register.Domain.Common;
using Vehicle.Register.Domain.Entities;
using Vehicle.Register.Domain.Repositories;

namespace Vehicle.Register.Infrastructure.Repositories
{
    public class VehicleRepository: IVehicleRepository
    {
        private readonly VehicleDBContext vehicleDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.vehicleDBContext; }
        }

        public VehicleRepository(VehicleDBContext vehicleDBContext)
        {
            this.vehicleDBContext = vehicleDBContext ?? throw new ArgumentNullException(nameof(vehicleDBContext));
        }

        public Domain.Aggregates.Vehicle SelectVehicle(int vehicleId)
        {
            return this.vehicleDBContext
                .Vehicles
                .Where(v => v.VehicleId == vehicleId)
                .FirstOrDefault();
        }

        public Domain.Aggregates.Vehicle Insert(Domain.Aggregates.Vehicle vehicle)
        {
            return this.vehicleDBContext
                   .Vehicles
                   .Add(vehicle)
                   .Entity;
        }

        public Domain.Aggregates.Vehicle Update(Domain.Aggregates.Vehicle vehicle)
        {
            return this.vehicleDBContext
                .Update(vehicle)
                .Entity;
        }

        public void Delete(Domain.Aggregates.Vehicle vehicle)
        {
            this.vehicleDBContext
                .Vehicles
                .Remove(vehicle);
        }

        public Brand SelectBrand(int brandId)
        {
            return this.vehicleDBContext
                   .Brands
                   .Where(v => v.BrandId == brandId)
                   .FirstOrDefault();
        }

        public Brand Insert(Brand brand)
        {
            return this.vehicleDBContext
                      .Brands
                      .Add(brand)
                      .Entity;
        }

        public Brand Update(Brand brand)
        {
            return this.vehicleDBContext
                   .Update(brand)
                   .Entity;
        }

        public void Delete(Brand brand)
        {
            this.vehicleDBContext
                   .Brands
                   .Remove(brand);
        }

        public VehicleType SelectVehicleType(int vehicleTypeId)
        {
            return this.vehicleDBContext
                   .VehicleTypes
                   .Where(v => v.VehicleTypeId == vehicleTypeId)
                   .FirstOrDefault();
        }

        public VehicleType Insert(VehicleType vehicleType)
        {
            return this.vehicleDBContext
                         .VehicleTypes
                         .Add(vehicleType)
                         .Entity;
        }

        public VehicleType Update(VehicleType vehicleType)
        {
            return this.vehicleDBContext
                   .Update(vehicleType)
                   .Entity;
        }

        public void Delete(VehicleType vehicleType)
        {
            this.vehicleDBContext
                   .VehicleTypes
                   .Remove(vehicleType);
        }

        public IEnumerable<Domain.Aggregates.Vehicle> TESTE(string name)
        {
            return this.vehicleDBContext
                .Vehicles
                .Where(v => v.Name == name);
        }
    }
}
