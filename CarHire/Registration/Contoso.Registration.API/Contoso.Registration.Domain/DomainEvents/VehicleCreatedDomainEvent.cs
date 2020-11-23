// <copyright file="VehicleCreatedDomainEvent.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using MediatR;

namespace Contoso.Registration.Domain.DomainEvents
{
    /// <summary>
    /// Vehicle created domain event.
    /// </summary>
    public class VehicleCreatedDomainEvent : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleCreatedDomainEvent"/> class.
        /// </summary>
        /// <param name="brand">Brand.</param>
        /// <param name="name">Name.</param>
        public VehicleCreatedDomainEvent(string brand, string name)
        {
            this.Brand = brand;
            this.Name = name;
        }

        /// <summary>
        /// Gets Brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets name.
        /// </summary>
        public string Name { get; }
    }
}
