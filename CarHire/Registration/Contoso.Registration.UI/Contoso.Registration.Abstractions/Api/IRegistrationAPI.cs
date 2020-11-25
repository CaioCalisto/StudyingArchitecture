// <copyright file="IRegistrationAPI.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Registration.Services.Api.Models;

namespace Contoso.Registration.Services.Api
{
    /// <summary>
    /// Registration api interface.
    /// </summary>
    public interface IRegistrationAPI
    {
        /// <summary>
        /// Get vehicles.
        /// </summary>
        /// <returns>Vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    }
}
