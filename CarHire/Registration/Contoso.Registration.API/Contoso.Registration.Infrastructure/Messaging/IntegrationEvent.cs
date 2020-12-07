// <copyright file="IntegrationEvent.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;

namespace Contoso.Registration.Infrastructure.Messaging
{
    /// <summary>
    /// Integration Event.
    /// </summary>
    public class IntegrationEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEvent"/> class.
        /// </summary>
        public IntegrationEvent()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets Message Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets Message CreateDate.
        /// </summary>
        public DateTime CreationDate { get; private set; }
    }
}
