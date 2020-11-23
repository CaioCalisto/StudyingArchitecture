// <copyright file="Entity.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using MediatR;

namespace Contoso.Registration.Domain.Common
{
    /// <summary>
    /// Represents the entity.
    /// </summary>
    public abstract class Entity
    {
        private List<INotification> domainEvents;

        /// <summary>
        /// Gets Domain event list.
        /// </summary>
        public IEnumerable<INotification> DomainEvents
        {
            get => this.domainEvents.AsEnumerable();
        }

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        protected void AddDomainEvent(INotification domainEvent)
        {
            if (this.domainEvents == null)
            {
                this.domainEvents = new List<INotification>();
            }

            this.domainEvents.Add(domainEvent);
        }
    }
}
