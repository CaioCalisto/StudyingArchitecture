using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using AutoMapper;
using Contoso.Registration.Infrastructure.Database;
using Microsoft.Azure.Cosmos.Table;

namespace Contoso.Registration.Application.Queries
{
    /// <summary>
    /// Queries.
    /// </summary>
    public class VehiclesQueries : IVehiclesQueries
    {
        private readonly IDatabaseQueries databaseQueries;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesQueries"/> class.
        /// </summary>
        /// <param name="databaseQueries">Database context.</param>
        /// <param name="mapper">Mapper.</param>
        public VehiclesQueries(IDatabaseQueries databaseQueries, IMapper mapper)
        {
            this.databaseQueries = databaseQueries;
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public IEnumerable<Model.Vehicle> Find(Model.Vehicle vehicle)
        {
            List<TableEntityAdapter<Infrastructure.Model.Vehicle>> result = (from entity in this.databaseQueries.GetQuery<Infrastructure.Model.Vehicle>()
                          where (string.IsNullOrEmpty(vehicle.Brand) || entity.OriginalEntity.Brand.Equals(vehicle.Brand)) &&
                          (string.IsNullOrEmpty(vehicle.Name) || entity.OriginalEntity.Name.Equals(vehicle.Name)) &&
                          (string.IsNullOrEmpty(vehicle.Transmission) || entity.OriginalEntity.Transmission.Equals(vehicle.Transmission)) &&
                          (string.IsNullOrEmpty(vehicle.Category) || entity.OriginalEntity.Category.Equals(vehicle.Category)) &&
                          (!vehicle.Consume.HasValue || entity.OriginalEntity.Consume.Equals(vehicle.Consume)) &&
                          (!vehicle.Passengers.HasValue || entity.OriginalEntity.Passengers.Equals(vehicle.Passengers)) &&
                          (!vehicle.Doors.HasValue || entity.OriginalEntity.Doors.Equals(vehicle.Doors)) &&
                          (!vehicle.Emission.HasValue || entity.OriginalEntity.Emission.Equals(vehicle.Emission))
                          select entity).ToList();

            return this.mapper.Map<IEnumerable<Infrastructure.Model.Vehicle>, IEnumerable<Model.Vehicle>>(result.Select(c => c.OriginalEntity));
        }
    }
}