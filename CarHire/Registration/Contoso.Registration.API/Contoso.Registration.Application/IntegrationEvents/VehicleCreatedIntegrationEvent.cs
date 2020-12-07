// <copyright file="VehicleCreatedIntegrationEvent.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using Contoso.Registration.Infrastructure.Messaging;

namespace Contoso.Registration.Application.IntegrationEvents
{
    /// <summary>
    /// VehicleCreated Integration Event.
    /// </summary>
    public class VehicleCreatedIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// Gets or sets Brand.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }
    }
}
