// <copyright file="IMessageBus.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace Contoso.Registration.Infrastructure.Messaging
{
    /// <summary>
    /// Message Bus.
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// Publish the event message.
        /// </summary>
        /// <param name="integrationEvent">Integration Event.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task Publish(IntegrationEvent integrationEvent);
    }
}
