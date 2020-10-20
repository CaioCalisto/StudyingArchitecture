// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

namespace Contoso.Registration.FunctionalTest.Model
{
    /// <summary>
    /// Database model vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets Brand.
        /// </summary>
        public string Brand { get; private set; }

        /// <summary>
        /// Gets Category.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets How many doors.
        /// </summary>
        public int Doors { get; private set; }

        /// <summary>
        /// Gets How many passengers.
        /// </summary>
        public int Passengers { get; private set; }

        /// <summary>
        /// Gets Gear transmission.
        /// </summary>
        public string Transmission { get; private set; }

        /// <summary>
        /// Gets Miles/Galon consume.
        /// </summary>
        public int Consume { get; private set; }

        /// <summary>
        /// Gets g/km CO2.
        /// </summary>
        public int Emission { get; private set; }
    }
}
