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
        /// Inexistent Category on Create Vehicle.
        /// </summary>
        [TestMethod]
        public void Create_InexistentCategory_ThrowException()
        {
            string category = "SomeOddcategory";
            try
            {
                Vehicle vehicle = Vehicle.Create(
                "Fiesta",
                "Ford",
                category,
                4,
                5,
                "Manual",
                45,
                23);

                Assert.Fail("Inexistent category should throw an error");
            }
            catch (Exception ex)
            {
                Assert.AreEqual($"Category {category} doest not exists", ex.Message);
            }
        }

        /// <summary>
        /// Inexistent Category on Create Vehicle.
        /// </summary>
        [TestMethod]
        public void Create_InexistentTransmission_ThrowException()
        {
            string transmission = "newKindOfGear";
            try
            {
                Vehicle vehicle = Vehicle.Create(
                "Fiesta",
                "Ford",
                "Standard",
                4,
                5,
                transmission,
                45,
                23);

                Assert.Fail("Inexistent transmission should throw an error");
            }
            catch (Exception ex)
            {
                Assert.AreEqual($"Transmission {transmission} doest not exists", ex.Message);
            }
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
