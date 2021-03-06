﻿// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

namespace Contoso.Registration.Application.Model
{
    /// <summary>
    /// Vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or Sets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets How many doors.
        /// </summary>
        public int? Doors { get; set; }

        /// <summary>
        /// Gets or Sets How many passengers.
        /// </summary>
        public int? Passengers { get; set; }

        /// <summary>
        /// Gets or Sets Gear transmission.
        /// </summary>
        public string Transmission { get; set; }

        /// <summary>
        /// Gets or Sets Miles/Galon consume.
        /// </summary>
        public int? Consume { get; set; }

        /// <summary>
        /// Gets or Sets g/km CO2.
        /// </summary>
        public int? Emission { get; set; }
    }
}
