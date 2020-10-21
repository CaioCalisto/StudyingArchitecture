// <copyright file="Vehicle.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.Domain.Aggregate;

namespace Contoso.Registration.Application.Stubs.Domain
{
    internal class Vehicle : Registration.Domain.Aggregate.Vehicle
    {
        internal void SetBrand(string brand)
        {
            this.Brand = brand;
        }

        internal void SetName(string name)
        {
            this.Name = name;
        }

        internal void SetCategory(Category category)
        {
            this.Category = category;
        }

        internal void SetDoors(int doors)
        {
            this.Doors = doors;
        }

        internal void SetPassengers(int passengers)
        {
            this.Passengers = passengers;
        }

        internal void SetTransmission(Transmission transmission)
        {
            this.Transmission = transmission;
        }

        internal void SetConsume(int consume)
        {
            this.Consume = consume;
        }

        internal void SetEmission(int emission)
        {
            this.Emission = emission;
        }
    }
}
