using Customer.Register.Application.Commands.Counties;
using Customer.Register.Application.Queries;
using Customer.Register.Domain.Aggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class CountiesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICountyQueries countyQueries;

        public CountiesController(IMediator mediator, ICountyQueries countyQueries)
        {
            this.mediator = mediator;
            this.countyQueries = countyQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<County>>> Get(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<County> result = await this.countyQueries.GetCountiesAsync(offset, next);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{countyId}")]
        public async Task<ActionResult<County>> GetCountyById(int countyId)
        {
            try
            {
                County county = await this.countyQueries.GetCountyByIdAsync(countyId);
                return Ok(county);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{countyId}/addresses/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetCountiesByCountry(int countyId, int offset, int next)
        {
            try
            {
                IEnumerable<Address> addresses = await this.countyQueries.GetAddressesByCounty(countyId, offset, next);
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<County>> CreateCounty(AddCountyCommand addCountyCommand)
        {
            try
            {
                County county = await this.mediator.Send(addCountyCommand);
                return this.Ok(county);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region PUT
        [HttpPut("{countyId}")]
        public async Task<ActionResult<County>> UpdateCounty(int countyId, UpdateCountyCommand updateCountyCommand)
        {
            try
            {
                updateCountyCommand.SetCountyId(countyId);
                County county = await this.mediator.Send(updateCountyCommand);
                return Ok(county);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion
    }
}
