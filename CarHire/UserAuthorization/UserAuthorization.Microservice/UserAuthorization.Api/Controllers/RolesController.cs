using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Roles;
using UserAuthorization.Application.Queries;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IRoleQueries roleQueries;

        public RolesController(IMediator mediator, IRoleQueries roleQueries)
        {
            this.mediator = mediator;
            this.roleQueries = roleQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Role> endUsers = await this.roleQueries.GetRolesAsync(offset, next);
                return Ok(endUsers);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult<Role>> GetRoleById(int roleId)
        {
            try
            {
                Role role = await this.roleQueries.GetRoleByRoleIdAsync(roleId);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{roleId}/endUsers/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<EndUser>>> GetEndUsers(int roleId, int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<EndUser> endUsers = await this.roleQueries.GetEndUsersByRoleIdAsync(roleId, offset, next);
                return Ok(endUsers);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{roleId}/permissions/{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetRolePermissions(int roleId, int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Permission> permissions = await this.roleQueries.GetPermissionsByRoleIdAsync(roleId, offset, next);
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
        public async Task<ActionResult<Role>> Create(AddRoleCommand addRoleCommand)
        {
            try
            {
                Role role = await this.mediator.Send(addRoleCommand);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteRoleCommand deleteRoleCommand)
        {
            try
            {
                bool success = await this.mediator.Send(deleteRoleCommand);
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
