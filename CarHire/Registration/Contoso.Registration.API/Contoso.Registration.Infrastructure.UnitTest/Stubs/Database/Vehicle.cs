// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

namespace Contoso.Registration.Infrastructure.Stubs.Database
{
    /// <summary>
    /// DB Vehicle stub.
    /// </summary>
    internal class Vehicle : Model.Vehicle
    {
        public Vehicle(string name, string brand, string category, int doors, int passengers, string transmission, int consume, int emission)
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
    }
}
