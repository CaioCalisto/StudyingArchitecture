// <copyright file="IVehiclesQueries.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
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
        /// <returns>Vehicles.</returns>
        IEnumerable<Vehicle> Find(Vehicle vehicle);
    }
}