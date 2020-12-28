// <copyright file="VehiclesController.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading.Tasks;
using Contoso.Registration.Api.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Contoso.Registration.Api.Controllers
{
    /// <summary>
    /// Vehicles controller.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly Application.Queries.IVehiclesQueries vehiclesQueries;
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="vehiclesQueries">Queries.</param>
        /// <param name="mediator">Mediator.</param>
        public VehiclesController(Application.Queries.IVehiclesQueries vehiclesQueries, IMediator mediator)
        {
            this.vehiclesQueries = vehiclesQueries;
            this.mediator = mediator;
        }

        /// <summary>
        /// Get vehicles by paramaters.
        /// </summary>
        /// <param name="vehicles">Parameters.</param>
        /// <param name="pagination">Pagination.</param>
        /// <returns>Vehicles.</returns>
        [HttpGet]
        [Route("query")]
        public IActionResult GetVehicles([FromQuery] Application.Queries.Parameters.Vehicle vehicles, [FromQuery] Application.Model.Pagination pagination)
        {
            Application.Model.PagedList<Application.Model.Vehicle> result = this.vehiclesQueries.Find(vehicles, pagination);
            this.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(new
            {
                result.Total,
                result.Page,
                result.Limit,
            }));

            return result.Count() == 0
                ? this.NotFound()
                : this.Ok(result);
        }

        /// <summary>
        /// Create new vehicle.
        /// </summary>
        /// <param name="addVehicleCommand">Command.</param>
        /// <returns>Vehicle created.</returns>
        [HttpPost]
        [Authorize(Policy = Policies.CanEdit)]
        public async Task<IActionResult> Create([FromBody] Application.Commands.AddVehicleCommand addVehicleCommand) => this.Ok(await this.mediator.Send(addVehicleCommand));
    }
}