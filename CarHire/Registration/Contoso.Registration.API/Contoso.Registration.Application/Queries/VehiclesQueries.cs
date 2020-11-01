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
            IQueryable<TableEntityAdapter<Infrastructure.Model.Vehicle>> query = this.databaseQueries.GetQuery<Infrastructure.Model.Vehicle>();

            List<TableEntityAdapter<Infrastructure.Model.Vehicle>> result = query
                .Where(q => (string.IsNullOrEmpty(vehicle.Brand) || q.OriginalEntity.Brand == vehicle.Brand) &&
                (string.IsNullOrEmpty(vehicle.Name) || q.OriginalEntity.Name == vehicle.Name) &&
                (string.IsNullOrEmpty(vehicle.Category) || q.OriginalEntity.Category == vehicle.Category) &&
                (string.IsNullOrEmpty(vehicle.Transmission) || q.OriginalEntity.Transmission == vehicle.Transmission))
                .ToList();

            return this.mapper.Map<IEnumerable<Infrastructure.Model.Vehicle>, IEnumerable<Model.Vehicle>>(result.Select(c => c.OriginalEntity));
        }
    }
}