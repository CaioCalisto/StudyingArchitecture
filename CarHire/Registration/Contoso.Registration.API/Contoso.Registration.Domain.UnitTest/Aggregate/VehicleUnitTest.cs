// <copyright file="VehicleUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Contoso.Registration.Domain.DomainEvents;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contoso.Registration.Domain.Aggregate
{
    /// <summary>
    /// Unit test for Vehicle.
    /// </summary>
    [TestClass]
    public class VehicleUnitTest
    {
        /// <summary>
        /// Create new Vehicle.
        /// </summary>
        [TestMethod]
        public void Create_NewVehicle_ReturnObject()
        {
            Vehicle vehicle = this.GetValidVehicle();

            Assert.AreEqual("Fiesta", vehicle.Name);
            Assert.AreEqual("Ford", vehicle.Brand);
            Assert.AreEqual(Category.STANDARD, vehicle.Category);
            Assert.AreEqual(4, vehicle.Doors);
            Assert.AreEqual(5, vehicle.Passengers);
            Assert.AreEqual(Transmission.MANUAL, vehicle.Transmission);
            Assert.AreEqual(45, vehicle.Consume);
            Assert.AreEqual(23, vehicle.Emission);
        }

        /// <summary>
        /// Create new Vehicle.
        /// </summary>
        [TestMethod]
        public void Create_NewVehicle_DomainEventCreated()
        {
            Vehicle vehicle = this.GetValidVehicle();
            VehicleCreatedDomainEvent domainEvent = (VehicleCreatedDomainEvent)vehicle.DomainEvents.FirstOrDefault();

            Assert.IsNotNull(domainEvent);
            Assert.AreEqual("Ford", domainEvent.Brand);
            Assert.AreEqual("Fiesta", domainEvent.Name);
        }

        /// <summary>
        /// Delete Event.
        /// </summary>
        [TestMethod]
        public void DeleteEvent_ContainsOneEvent_ListShouldContainsZeroEvents()
        {
            Vehicle vehicle = this.GetValidVehicle();
            INotification domainEvent = vehicle.DomainEvents.FirstOrDefault();
            vehicle.RemoveDomainEvent(domainEvent);

            Assert.AreEqual(0, vehicle.DomainEvents.Count());
        }

        private Vehicle GetValidVehicle() => Vehicle.Create(
                "Fiesta",
                "Ford",
                "Standard",
                4,
                5,
                "Manual",
                45,
                23);
    }
}
