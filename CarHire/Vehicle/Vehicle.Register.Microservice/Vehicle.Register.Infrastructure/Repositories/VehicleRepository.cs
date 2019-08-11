using System;
using System.Collections.Generic;
using System.Text;
using Vehicle.Register.Domain.Common;
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
    }
}
