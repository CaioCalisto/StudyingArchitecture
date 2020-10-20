// <copyright file="AddVehicleCommandHandlerUnitTest.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.Registration.Application.Mappers;
using Contoso.Registration.Domain.Common;
using Contoso.Registration.Domain.Ports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;

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
        /// Initializes a new instance of the <see cref="AddVehicleCommandHandlerUnitTest"/> class.
        /// </summary>
        public AddVehicleCommandHandlerUnitTest()
        {
            this.mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapProfile>()).CreateMapper();
        }

        /// <summary>
        /// Handle test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [TestMethod]
        public async Task Handle_NewVehicle_SaveInDB()
        {
            Mock<IVehicleRepositoy> repositoryMock = new Mock<IVehicleRepositoy>();
            Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.SaveEntitiesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            repositoryMock.Setup(r => r.InsertAsync(It.IsAny<Domain.Aggregate.Vehicle>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(this.GetVehicle());
            AddVehicleCommandHandler handler = new AddVehicleCommandHandler(repositoryMock.Object, this.mapper);
            var result = await handler.Handle(this.GetCommand(), CancellationToken.None);

            repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Domain.Aggregate.Vehicle>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
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

        private Domain.Aggregate.Vehicle GetVehicle()
        {
            Domain.Aggregate.Vehicle vehicle = (Domain.Aggregate.Vehicle)Activator.CreateInstance(typeof(Domain.Aggregate.Vehicle), true);
            return vehicle;
        }
    }
}
