// <copyright file="VehicleCreatedDomainEventHandler.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Contoso.Registration.Application.IntegrationEvents;
using Contoso.Registration.Domain.DomainEvents;
using Contoso.Registration.Infrastructure.Messaging;
using MediatR;

namespace Contoso.Registration.Application.DomainEventHandler
{
    /// <summary>
    /// Vehicle Created Domain Event Handler.
    /// </summary>
    public class VehicleCreatedDomainEventHandler : INotificationHandler<VehicleCreatedDomainEvent>
    {
        private readonly IMessageBus messageBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleCreatedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="messageBus">Message bus.</param>
        public VehicleCreatedDomainEventHandler(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        /// <inheritdoc/>
        public async Task Handle(VehicleCreatedDomainEvent notification, CancellationToken cancellationToken) =>
            await this.messageBus.PublishAsync(new VehicleCreatedIntegrationEvent()
            {
                Name = notification.Name,
                Brand = notification.Brand,
            });
    }
}