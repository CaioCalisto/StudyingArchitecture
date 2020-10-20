// <copyright file="AddVehicleCommandHandler.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.Registration.Application.Model;
using Contoso.Registration.Domain.Ports;
using MediatR;

namespace Contoso.Registration.Application.Commands
{
    /// <summary>
    /// Handle the AddVehicle command.
    /// </summary>
    public class AddVehicleCommandHandler : IRequestHandler<AddVehicleCommand, Vehicle>
    {
        private readonly IVehicleRepositoy vehicleRepositoy;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleCommandHandler"/> class.
        /// </summary>
        /// <param name="vehicleRepositoy">Vehicle repository.</param>
        /// <param name="mapper">Model mapper.</param>
        public AddVehicleCommandHandler(IVehicleRepositoy vehicleRepositoy, IMapper mapper)
        {
            this.vehicleRepositoy = vehicleRepositoy;
            this.mapper = mapper;
        }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <param name="request">Command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Vehicle.</returns>
        public async Task<Vehicle> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
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

            vehicle = await this.vehicleRepositoy.InsertAsync(vehicle, vehicle.Brand, $"{vehicle.Brand} {vehicle.Name} {vehicle.Category.ToString().ToUpper()}");
            return this.mapper.Map<Model.Vehicle>(vehicle);
        }
    }
}
