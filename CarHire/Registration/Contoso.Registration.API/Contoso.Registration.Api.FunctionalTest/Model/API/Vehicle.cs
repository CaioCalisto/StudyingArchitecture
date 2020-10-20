// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Newtonsoft.Json;

namespace Contoso.Registration.FunctionalTest.Model.API
{
    /// <summary>
    /// Database model vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle()
        {
        }

        /// <summary>
        /// Gets Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets Brand.
        /// </summary>
        [JsonProperty("brand")]
        public string Brand { get; private set; }

        /// <summary>
        /// Gets Category.
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; private set; }

        /// <summary>
        /// Gets How many doors.
        /// </summary>
        [JsonProperty("doors")]
        public int Doors { get; private set; }

        /// <summary>
        /// Gets How many passengers.
        /// </summary>
        [JsonProperty("passengers")]
        public int Passengers { get; private set; }

        /// <summary>
        /// Gets Gear transmission.
        /// </summary>
        [JsonProperty("transmission")]
        public string Transmission { get; private set; }

        /// <summary>
        /// Gets Miles/Galon consume.
        /// </summary>
        [JsonProperty("consume")]
        public int Consume { get; private set; }

        /// <summary>
        /// Gets g/km CO2.
        /// </summary>
        [JsonProperty("emission")]
        public int Emission { get; private set; }
    }
}
