using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using AutoMapper;
using Contoso.Registration.Infrastructure.Database;
using Microsoft.Azure.Cosmos.Linq;
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
                query = query.Where(v => v.OriginalEntity.Brand.Equals(vehicle.Brand));
            }

            if (!string.IsNullOrEmpty(vehicle.Name))
            {
                query = query.Where(v => v.OriginalEntity.Name.Equals(vehicle.Name));
            }

            if (!string.IsNullOrEmpty(vehicle.Category))
            {
                query = query.Where(v => v.OriginalEntity.Category.Equals(vehicle.Category));
            }

            if (!string.IsNullOrEmpty(vehicle.Transmission))
            {
                query = query.Where(v => v.OriginalEntity.Transmission.Equals(vehicle.Transmission));
            }

            if (vehicle.Doors.HasValue)
            {
                query = query.Where(v => v.OriginalEntity.Doors.Equals(vehicle.Doors));
            }

            if (vehicle.Consume.HasValue)
            {
                query = query.Where(v => v.OriginalEntity.Consume.Equals(vehicle.Consume));
            }

            if (vehicle.Emission.HasValue)
            {
                query = query.Where(v => v.OriginalEntity.Emission.Equals(vehicle.Emission));
            }

            List<TableEntityAdapter<Infrastructure.Model.Vehicle>> result = query.ToList();

            return this.mapper.Map<IEnumerable<Infrastructure.Model.Vehicle>, IEnumerable<Model.Vehicle>>(result.Select(c => c.OriginalEntity));
        }
    }
}