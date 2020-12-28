// <copyright file="IVehiclesQueries.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.Application.Model;

namespace Contoso.Registration.Application.Queries
{
    /// <summary>
    /// Queries interface.
    /// </summary>
    public interface IVehiclesQueries
    {
        /// <summary>
        /// Find vehicles.
        /// </summary>
        /// <param name="vehicle">Parameters.</param>
        /// <param name="pagination">Pagination.</param>
        /// <returns>Vehicles.</returns>
        PagedList<Vehicle> Find(Parameters.Vehicle vehicle, Pagination pagination);
    }
}