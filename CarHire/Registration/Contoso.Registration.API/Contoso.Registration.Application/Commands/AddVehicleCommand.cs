// <copyright file="AddVehicleCommand.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Contoso.Registration.Application.Model;
using MediatR;
using Newtonsoft.Json;

namespace Contoso.Registration.Application.Commands
{
    /// <summary>
    /// Add vehicle command.
    /// </summary>
    public class AddVehicleCommand : IRequest<IEnumerable<Vehicle>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleCommand"/> class.
        /// </summary>
        public AddVehicleCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleCommand"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="brand">Brand.</param>
        /// <param name="category">Category.</param>
        /// <param name="doors">Doors.</param>
        /// <param name="passengers">Passengers.</param>
        /// <param name="transmission">Transmission.</param>
        /// <param name="consume">Consume.</param>
        /// <param name="emission">Emission.</param>
        public AddVehicleCommand(string name, string brand, string category, int doors, int passengers, string transmission, int consume, int emission)
        {
            this.Name = name;
            this.Brand = brand;
            this.Category = category;
            this.Doors = doors;
            this.Passengers = passengers;
            this.Transmission = transmission;
            this.Consume = consume;
            this.Emission = emission;
        }

        /// <summary>
        /// Gets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets Category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets How many doors.
        /// </summary>
        public int Doors { get; set; }

        /// <summary>
        /// Gets How many passengers.
        /// </summary>
        public int Passengers { get; set; }

        /// <summary>
        /// Gets Gear transmission.
        /// </summary>
        public string Transmission { get; set; }

        /// <summary>
        /// Gets Miles/Galon consume.
        /// </summary>
        public int Consume { get; set; }

        /// <summary>
        /// Gets g/km CO2.
        /// </summary>
        public int Emission { get; set; }
    }
}
