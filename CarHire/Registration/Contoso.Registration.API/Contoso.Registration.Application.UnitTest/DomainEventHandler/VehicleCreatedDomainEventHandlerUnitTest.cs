// <copyright file="VehicleCreatedDomainEventHandlerUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Contoso.Registration.Application.IntegrationEvents;
using Contoso.Registration.Domain.DomainEvents;
using Contoso.Registration.Infrastructure.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contoso.Registration.Application.DomainEventHandler
{
    /// <summary>
    /// Test for VehicleCreatedDomainEventHandler.
    /// </summary>
    [TestClass]
    public class VehicleCreatedDomainEventHandlerUnitTest
    {
        /// <summary>
        /// Unit test for Handler.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_ShouldSendMessage()
        {
            Mock<IMessageBus> busMock = new Mock<IMessageBus>();
            VehicleCreatedDomainEventHandler handler = new VehicleCreatedDomainEventHandler(busMock.Object);
            await handler.Handle(new VehicleCreatedDomainEvent("Ford", "Fiesta"), CancellationToken.None);

            busMock.Verify(
                bus =>
                bus.PublishAsync(It.Is<IntegrationEvent>(e =>
                ((VehicleCreatedIntegrationEvent)e).Brand.Equals("Ford") &&
                ((VehicleCreatedIntegrationEvent)e).Name.Equals("Fiesta"))),
                Times.Once);
        }
    }
}
