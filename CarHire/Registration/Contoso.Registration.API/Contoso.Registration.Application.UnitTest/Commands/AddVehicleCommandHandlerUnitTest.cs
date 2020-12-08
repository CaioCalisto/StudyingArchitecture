// <copyright file="AddVehicleCommandHandlerUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.Registration.Application.Mappers;
using Contoso.Registration.Domain.Common;
using Contoso.Registration.Domain.Ports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Contoso.Registration.Application.Commands
{
    /// <summary>
    /// Unit Test for Add Vehicle Command.
    /// </summary>
    [TestClass]
    public class AddVehicleCommandHandlerUnitTest
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
        /// Handler test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_NewVehicle_SaveInDB()
        {
            Mock<IVehicleRepository> repositoryMock = new Mock<IVehicleRepository>();
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            repositoryMock.Setup(r => r.InsertAsync(It.IsAny<Domain.Aggregate.Vehicle>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(this.GetDomainVehicle());
            AddVehicleCommandHandler handler = new AddVehicleCommandHandler(repositoryMock.Object, this.mapper);
            IEnumerable<Model.Vehicle> result = await handler.Handle(this.GetCommand(), CancellationToken.None);

            repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Domain.Aggregate.Vehicle>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Handler test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_NewVehicle_ShouldMapCorrectly()
        {
            Mock<IVehicleRepository> repositoryMock = new Mock<IVehicleRepository>();
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            repositoryMock.Setup(r => r.InsertAsync(It.IsAny<Domain.Aggregate.Vehicle>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(this.GetDomainVehicle());
            AddVehicleCommandHandler handler = new AddVehicleCommandHandler(repositoryMock.Object, this.mapper);
            IEnumerable<Model.Vehicle> result = await handler.Handle(this.GetCommand(), CancellationToken.None);

            Assert.AreEqual("Ford", result.First().Brand);
            Assert.AreEqual("Fiesta", result.First().Name);
            Assert.AreEqual("STANDARD", result.First().Category.ToUpper());
            Assert.AreEqual(5, result.First().Passengers);
            Assert.AreEqual(4, result.First().Doors);
            Assert.AreEqual("MANUAL", result.First().Transmission.ToUpper());
            Assert.AreEqual(23, result.First().Consume);
            Assert.AreEqual(12, result.First().Emission);
        }

        private AddVehicleCommand GetCommand()
        {
            return new AddVehicleCommand()
            {
                Name = "Fiesta",
                Brand = "Ford",
                Category = "Standard",
                Doors = 4,
                Passengers = 5,
                Transmission = "manual",
                Consume = 23,
                Emission = 12,
            };
        }

        private Domain.Aggregate.Vehicle GetDomainVehicle()
        {
            Stubs.Domain.Vehicle vehicle = new Stubs.Domain.Vehicle();
            vehicle.SetBrand("Ford");
            vehicle.SetName("Fiesta");
            vehicle.SetCategory(Domain.Aggregate.Category.STANDARD);
            vehicle.SetPassengers(5);
            vehicle.SetDoors(4);
            vehicle.SetTransmission(Domain.Aggregate.Transmission.MANUAL);
            vehicle.SetConsume(23);
            vehicle.SetEmission(12);
            return vehicle;
        }
    }
}