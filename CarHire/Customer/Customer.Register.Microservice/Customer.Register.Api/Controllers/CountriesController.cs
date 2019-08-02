using Customer.Register.Application.Commands.Countries;
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
    public class CountriesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICountryQueries countryQueries;

        public CountriesController(IMediator mediator, ICountryQueries countryQueries)
        {
            this.mediator = mediator;
            this.countryQueries = countryQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Country>>> Get(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Country> result = await this.countryQueries.GetCountriesAsync(offset, next);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<Country>> GetCountryById(int countryId)
        {
            try
            {
                Country country = await this.countryQueries.GetCountryByIdAsync(countryId);
                return Ok(country);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{countryId}/counties/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<County>>> GetCountiesByCountry(int countryId, int offset, int next)
        {
            try
            {
                IEnumerable<County> counties = await this.countryQueries.GetCountiesAsync(countryId, offset, next);
                return Ok(counties);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(AddCountryCommand addCountryCommand)
        {
            try
            {
                Country country = await this.mediator.Send(addCountryCommand);
                return this.Ok(country);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region PUT
        [HttpPut]
        public async Task<ActionResult<Country>> UpdateCountry(UpdateCountryCommand updateCountryCommand)
        {
            try
            {
                Country country = await this.mediator.Send(updateCountryCommand);
                return Ok(country);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion
    }
}
