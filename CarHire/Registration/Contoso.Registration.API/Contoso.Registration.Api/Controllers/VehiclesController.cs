// <copyright file="VehiclesController.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Contoso.Registration.Application.Commands;
using Contoso.Registration.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contoso.Registration.Api.Controllers
{
    /// <summary>
    /// Vehicles controller.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> logger;
        private readonly IVehiclesQueries vehiclesQueries;
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="vehiclesQueries">Queries.</param>
        /// <param name="mediator">Mediator.</param>
        public VehiclesController(ILogger<VehiclesController> logger, IVehiclesQueries vehiclesQueries, IMediator mediator)
        {
            this.logger = logger;
            this.vehiclesQueries = vehiclesQueries;
            this.mediator = mediator;
        }

        /// <summary>
        /// Get all vehicles.
        /// </summary>
        /// <returns>Result.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return this.Ok();
        }

        /// <summary>
        /// Get vehicles by paramaters.
        /// </summary>
        /// <param name="vehicles">Parameters</param>
        /// <returns>Vehicles.</returns>
        [HttpGet]
        [Route("query")]
        public IActionResult GetVehicles([FromQuery] Application.Model.Vehicle vehicles)
        {
            return this.Ok(this.vehiclesQueries.Find(vehicles));
        }

        /// <summary>
        /// Create new vehicle.
        /// </summary>
        /// <param name="addVehicleCommand">Command.</param>
        /// <returns>Vehicle created.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddVehicleCommand addVehicleCommand)
        {
            Application.Model.Vehicle result = await this.mediator.Send(addVehicleCommand);
            return this.Ok(result);
        }
    }
}