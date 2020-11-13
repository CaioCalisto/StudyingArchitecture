// <copyright file="MapProfileUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Contoso.Registration.Infrastructure.Mappers
{
    /// <summary>
    /// Unit test for auto mapper.
    /// </summary>
    [TestClass]
    public class MapProfileUnitTest
    {
        private IMapper mapper;

        /// <summary>
        /// Setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>()).CreateMapper();
        }

        /// <summary>
        /// Map test.
        /// </summary>
        [TestMethod]
        public void MapFromStorageToDomain_ShouldMapCorrectly()
        {
            Stubs.Database.Vehicle vehicle = new Stubs.Database.Vehicle(
                "147", "Fiat", "Standard", 2, 4, "Manual", 10, 20);

            Domain.Aggregate.Vehicle result = this.mapper.Map<Domain.Aggregate.Vehicle>(vehicle);

            Assert.AreEqual("Fiat", result.Brand);
            Assert.AreEqual("147", result.Name);
            Assert.AreEqual(Domain.Aggregate.Category.STANDARD, result.Category);
            Assert.AreEqual(Domain.Aggregate.Transmission.MANUAL, result.Transmission);
            Assert.AreEqual(2, result.Doors);
            Assert.AreEqual(4, result.Passengers);
            Assert.AreEqual(10, result.Consume);
            Assert.AreEqual(20, result.Emission);
        }

        /// <summary>
        /// Map test.
        /// </summary>
        [TestMethod]
        public void MapFromStorageToDomain_IncorrectCategory_ShouldThrownError()
        {
            string category = "na";
            try
            {
                Stubs.Database.Vehicle vehicle = new Stubs.Database.Vehicle(
                "147", "Fiat", category, 2, 4, "Manual", 10, 20);

                Domain.Aggregate.Vehicle result = this.mapper.Map<Domain.Aggregate.Vehicle>(vehicle);
                Assert.Fail();
            }
            catch (AutoMapperMappingException ex)
            {
                Assert.AreEqual($"Requested value '{category}' was not found.", ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Map test.
        /// </summary>
        [TestMethod]
        public void MapFromStorageToDomain_IncorrectTransmission_ShouldThrownError()
        {
            string transmission = "NA";
            try
            {
                Stubs.Database.Vehicle vehicle = new Stubs.Database.Vehicle(
                "147", "Fiat", "Standard", 2, 4, transmission, 10, 20);

                Domain.Aggregate.Vehicle result = this.mapper.Map<Domain.Aggregate.Vehicle>(vehicle);
                Assert.Fail();
            }
            catch (AutoMapperMappingException ex)
            {
                Assert.AreEqual($"Requested value '{transmission}' was not found.", ex.InnerException.Message);
            }
        }
    }
}
