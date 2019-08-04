using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.EndUsers;
using UserAuthorization.Application.Queries;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class EndUsersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IEndUserQueries endUserQueries;

        public EndUsersController(IMediator mediator, IEndUserQueries endUserQueries)
        {
            this.mediator = mediator;
            this.endUserQueries = endUserQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<EndUser>>> GetAll(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<EndUser> endUsers = await this.endUserQueries.GetEndUsersAsync(offset, next);
                return Ok(endUsers);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<EndUser>> GetRoleById(string userName)
        {
            try
            {
                EndUser endUser = await this.endUserQueries.GetEndUserByUserNameAsync(userName);
                return Ok(endUser);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{endUserId}/roles/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles(int endUserId, int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Role> roles = await this.endUserQueries.GetRolesIdAsync(endUserId, offset, next);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{endUserId}/permissions/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetEndUserPermissions(int endUserId, int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Permission> permissions = await this.endUserQueries.GetPermissionsAsync(endUserId, offset, next);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{endUserId}/subDomains/{subDomainId}/permissions/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissionBySubDomain(int endUserId, int subDomainId, int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Permission> permissions = await this.endUserQueries.GetPermissionsAsync(endUserId, subDomainId, offset, next);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<ActionResult<EndUser>> Create(AddEndUserCommand addEndUserCommand)
        {
            try
            {
                EndUser endUser = await this.mediator.Send(addEndUserCommand);
                return Ok(endUser);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpPost("roles")]
        public async Task<ActionResult<bool>> AddRoleToUser(AddRoleToEndUserCommand addRoleToEndUserCommand)
        {
            try
            {
                bool success = await this.mediator.Send(addRoleToEndUserCommand);
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
        public async Task<ActionResult> Delete(DeleteEndUserCommand deleteEndUserCommand)
        {
            try
            {
                bool success = await this.mediator.Send(deleteEndUserCommand);
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
