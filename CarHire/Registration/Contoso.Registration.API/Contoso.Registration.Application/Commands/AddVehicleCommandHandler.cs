// <copyright file="AddVehicleCommandHandler.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contoso.Registration.Application.Model;
using Contoso.Registration.Domain.Ports;
using MediatR;

namespace Contoso.Registration.Application.Commands
{
    /// <summary>
    /// Handle the AddVehicle command.
    /// </summary>
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, IEnumerable<Vehicle>>
    {
        private readonly IVehicleRepository vehicleRepositoy;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepositoy">Vehicle repository.</param>
        public AddVehicleCommandHandler(IVehicleRepository vehicleRepositoy)
        {
            this.vehicleRepositoy = vehicleRepositoy;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Vehicle>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            Domain.Aggregate.Vehicle vehicle = Domain.Aggregate.Vehicle.Create(
                request.Name,
                request.Brand,
                request.Category,
                request.Doors,
                request.Passengers,
                request.Transmission,
                request.Consume,
                request.Emission);

            vehicle = await this.vehicleRepositoy.InsertAsync(vehicle);
            return this.Map(vehicle);
        }

        private IEnumerable<Vehicle> Map(Domain.Aggregate.Vehicle source) =>
            new List<Vehicle>()
            {
                new Vehicle()
                {
                    Brand = source.Brand,
                    Name = source.Name,
                    Category = source.Category.ToString(),
                    Transmission = source.Transmission.ToString(),
                    Doors = source.Doors,
                    Passengers = source.Passengers,
                    Consume = source.Consume,
                    Emission = source.Emission,
                },
            };
    }
}
