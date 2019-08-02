using Customer.Register.Application.Commands.Customers;
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
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICustomerQueries customerQueries;

        public CustomersController(IMediator mediator, ICustomerQueries customerQueries)
        {
            this.mediator = mediator;
            this.customerQueries = customerQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Domain.Aggregate.Customer>>> Get(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Domain.Aggregate.Customer> result = await this.customerQueries.GetCostumersAsync(offset, next);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{identity}")]
        public async Task<ActionResult<Domain.Aggregate.Customer>> GetCustomerByIdentity(int identity)
        {
            try
            {
                Domain.Aggregate.Customer customer = await this.customerQueries.GetCostumerByIdentityAsync(identity);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{identity}/address")]
        public async Task<ActionResult<Address>> GetCustomerAddress(int identity)
        {
            try
            {
                Address address = await this.customerQueries.GetCustomerAddressAsync(identity);
                return this.Ok(address);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult> CreateCostumer(AddCustomerCommand addCostumerCommand)
        {
            try
            {
                Domain.Aggregate.Customer costumer = await this.mediator.Send(addCostumerCommand);
                return this.Ok(costumer);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpPost("{identity}/address")]
        public async Task<ActionResult<bool>> AddCustomerAddress(int identity, AddCustomerAddressCommand addCustomerAddressCommand)
        {
            try
            {
                addCustomerAddressCommand.SetCustomerIdentity(identity);
                bool result = await this.mediator.Send(addCustomerAddressCommand);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region PUT
        [HttpPut("{identity}")]
        public async Task<ActionResult> UpdateCostumer(int identity, UpdateCustomerCommand updateCostumerCommand)
        {
            try
            {
                updateCostumerCommand.SetIdentity(identity);
                Domain.Aggregate.Customer costumer = await this.mediator.Send(updateCostumerCommand);
                return this.Ok(costumer);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion
    }
}