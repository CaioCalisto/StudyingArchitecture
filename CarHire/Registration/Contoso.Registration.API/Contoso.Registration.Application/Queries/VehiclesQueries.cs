using System.Collections.Generic;
using System.Linq;
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
            IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>> query = this.databaseQueries.GetQuery<Infrastructure.Model.Vehicle>();
            if (!string.IsNullOrEmpty(vehicle.Brand))
            {
                query = query.Where(c => c.OriginalEntity.Brand == vehicle.Brand);
            }

            if (!string.IsNullOrEmpty(vehicle.Name))
            {
                query = query.Where(c => c.OriginalEntity.Name == vehicle.Name);
            }

            if (!string.IsNullOrEmpty(vehicle.Transmission))
            {
                query = query.Where(c => c.OriginalEntity.Transmission == vehicle.Transmission);
            }

            if (!string.IsNullOrEmpty(vehicle.Category))
            {
                query = query.Where(c => c.OriginalEntity.Category == vehicle.Category);
            }

            if (vehicle.Passengers.HasValue)
            {
                query = query.Where(c => c.OriginalEntity.Passengers == vehicle.Passengers);
            }

            if (vehicle.Doors.HasValue)
            {
                query = query.Where(c => c.OriginalEntity.Doors == vehicle.Doors);
            }

            if (vehicle.Consume.HasValue)
            {
                query = query.Where(c => c.OriginalEntity.Consume == vehicle.Consume);
            }

            if (vehicle.Emission.HasValue)
            {
                query = query.Where(c => c.OriginalEntity.Emission == vehicle.Emission);
            }

            IEnumerable<Infrastructure.Model.Vehicle> result = query.ToList().Select(c => c.OriginalEntity);
            return this.mapper.Map<IEnumerable<Infrastructure.Model.Vehicle>, IEnumerable<Model.Vehicle>>(result);
        }
    }
}
