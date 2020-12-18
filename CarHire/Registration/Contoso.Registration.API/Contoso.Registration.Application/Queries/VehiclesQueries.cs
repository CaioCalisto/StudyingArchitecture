// <copyright file="VehiclesQueries.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesQueries"/> class.
        /// </summary>
        /// <param name="databaseQueries">Database context.</param>
        public VehiclesQueries(IDatabaseQueries databaseQueries)
        {
            this.databaseQueries = databaseQueries;
        }

        /// <inheritdoc/>
        public Model.PagedList<Model.Vehicle> Find(Model.Vehicle vehicle, Model.Pagination pagination)
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

            return this.Map(Model.PagedList<Infrastructure.Model.Vehicle>.ToPagedList(query, pagination.Page, pagination.Limit));
        }

        private Model.PagedList<Model.Vehicle> Map(Model.PagedList<Infrastructure.Model.Vehicle> source)
        {
            List<Model.Vehicle> vehicles = new List<Model.Vehicle>();
            foreach (var item in source)
            {
                vehicles.Add(new Model.Vehicle()
                {
                    Brand = item.Brand,
                    Name = item.Name,
                    Category = item.Category,
                    Transmission = item.Transmission,
                    Doors = item.Doors,
                    Passengers = item.Passengers,
                    Consume = item.Consume,
                    Emission = item.Emission,
                });
            }

            return new Model.PagedList<Model.Vehicle>(vehicles, source.Page, source.Limit, source.Total);
        }
    }
}