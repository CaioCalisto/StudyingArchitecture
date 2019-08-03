using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.SubDomains;
using UserAuthorization.Application.Queries;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class SubDomainsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ISubDomainQueries subDomainQueries;

        public SubDomainsController(IMediator mediator, ISubDomainQueries subDomainQueries)
        {
            this.mediator = mediator;
            this.subDomainQueries = subDomainQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<SubDomain>>> GetAll(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<SubDomain> subDomains = await this.subDomainQueries.GetSubDomainsAsync(offset, next);
                return Ok(subDomains);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{subDomainId}")]
        public async Task<ActionResult<SubDomain>> GetById(int subDomainId)
        {
            try
            {
                SubDomain subDomain = await this.subDomainQueries.GetSubDomainByIdAsync(subDomainId);
                return Ok(subDomain);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{subDomainId}/roles/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(int subDomainId, int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Role> roles = await this.subDomainQueries.GetRolesBySubDomainIdAsync(subDomainId, offset, next);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<SubDomain>> Create(AddSubDomainCommand addSubDomainCommand)
        {
            try
            {
                SubDomain subDomain = await this.mediator.Send(addSubDomainCommand);
                return Ok(subDomain);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpPost("roles")]
        public async Task<ActionResult<bool>> AddRoleToSubDomain(AddRoleToSubDomainCommand addRoleToSubDomainCommand)
        {
            try
            {
                bool success = await this.mediator.Send(addRoleToSubDomainCommand);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteSubDomainCommand deleteSubDomainCommand)
        {
            try
            {
                bool success = await this.mediator.Send(deleteSubDomainCommand);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion
    }
}
