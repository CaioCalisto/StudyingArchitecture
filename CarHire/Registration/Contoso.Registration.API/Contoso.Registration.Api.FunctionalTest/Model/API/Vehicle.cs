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
        /// Gets or Sets Name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Brand.
        /// </summary>
        [JsonProperty("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or Sets Category.
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or Sets How many doors.
        /// </summary>
        [JsonProperty("doors")]
        public int Doors { get; set; }

        /// <summary>
        /// Gets or Sets How many passengers.
        /// </summary>
        [JsonProperty("passengers")]
        public int Passengers { get; set; }

        /// <summary>
        /// Gets or Sets Gear transmission.
        /// </summary>
        [JsonProperty("transmission")]
        public string Transmission { get; set; }

        /// <summary>
        /// Gets or Sets Miles/Galon consume.
        /// </summary>
        [JsonProperty("consume")]
        public int Consume { get; set; }

        /// <summary>
        /// Gets or Sets g/km CO2.
        /// </summary>
        [JsonProperty("emission")]
        public int Emission { get; set; }
    }
}
