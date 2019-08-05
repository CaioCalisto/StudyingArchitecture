using Customer.Register.Application.Commands.Addresses;
using Customer.Register.Application.Models;
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
    public class AddressesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAddressQueries addressQueries;

        public AddressesController(IMediator mediator, IAddressQueries addressQueries)
        {
            this.mediator = mediator;
            this.addressQueries = addressQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<PaginatedResult<Address>>> GetAddresses(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                PaginatedResult<Address> result = await this.addressQueries.GetAddressesAsync(offset, next);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{addressId}")]
        public async Task<ActionResult<Address>> GetAddressById(int addressId)
        {
            try
            {
                Address address = await this.addressQueries.GetAddressByIdAsync(addressId);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion
        
        #region POST
        [HttpPost]
        public async Task<ActionResult<Address>> CreateAddress(AddAddressCommand addAddressCommand)
        {
            try
            {
                Address address = await this.mediator.Send(addAddressCommand);
                return this.Ok(address);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region PUT
        [HttpPut("{addressId}")]
        public async Task<ActionResult<Address>> UpdateAddress(int addressId, UpdateAddressCommand updateAddressCommand)
        {
            try
            {
                updateAddressCommand.SetAddressId(addressId);
                Address address = await this.mediator.Send(updateAddressCommand);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion
    }
}