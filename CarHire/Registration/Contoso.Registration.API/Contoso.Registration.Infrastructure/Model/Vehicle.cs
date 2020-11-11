// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Microsoft.Azure.Cosmos.Table;

namespace Contoso.Registration.Infrastructure.Model
{
    /// <summary>
    /// Database entity vehicle.
    /// </summary>
    public class Vehicle : TableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
        {
        }

        /// <summary>
        /// Gets or Sets Name.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets or Sets Brand.
        /// </summary>
        public string Brand { get; protected set; }

        /// <summary>
        /// Gets or Sets Category.
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        /// Gets or Sets How many doors.
        /// </summary>
        public int Doors { get; protected set; }

        /// <summary>
        /// Gets or Sets How many passengers.
        /// </summary>
        public int Passengers { get; protected set; }

        /// <summary>
        /// Gets or Sets Gear transmission.
        /// </summary>
        public string Transmission { get; protected set; }

        /// <summary>
        /// Gets or Sets Miles/Galon consume.
        /// </summary>
        public int Consume { get; protected set; }

        /// <summary>
        /// Gets or Sets g/km CO2.
        /// </summary>
        public int Emission { get; protected set; }
    }
}
