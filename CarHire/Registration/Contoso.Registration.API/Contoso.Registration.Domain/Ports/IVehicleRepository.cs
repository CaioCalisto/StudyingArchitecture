﻿// <copyright file="IVehicleRepository.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Contoso.Registration.Domain.Aggregate;
using Contoso.Registration.Domain.Common;

namespace Contoso.Registration.Domain.Ports
{
    /// <summary>
    /// Vehicle Repository interface.
    /// </summary>
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        /// <summary>
        /// Insert new entity in database.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="entity">Entity.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<Vehicle> InsertAsync(Vehicle entity);
    }
}
