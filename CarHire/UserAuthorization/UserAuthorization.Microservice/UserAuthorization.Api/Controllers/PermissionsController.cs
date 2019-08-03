using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Permissions;
using UserAuthorization.Application.Queries;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPermissionQueries permissionQueries;

        public PermissionsController(IMediator mediator, IPermissionQueries permissionQueries)
        {
            this.mediator = mediator;
            this.permissionQueries = permissionQueries;
        }

        #region GET
        [HttpGet("{offset}/{next}")]
        public async Task<ActionResult<IEnumerable<Permission>>> GetAll(int offset, int next)
        {
            try
            {
                offset = offset == 0 ? 0 : offset;
                next = next == 0 ? 10 : next;
                IEnumerable<Permission> permissions = await this.permissionQueries.GetPermissionsAsync(offset, next);
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        [HttpGet("{permissionId}")]
        public async Task<ActionResult<Permission>> GetPermissionById(int permissionId)
        {
            try
            {
                Permission permission = await this.permissionQueries.GetPermissionByIdAsync(permissionId);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region POST
        public async Task<ActionResult<Permission>> Create(AddPermissionCommand addPermissionCommand)
        {
            try
            {
                Permission permission = await this.mediator.Send(addPermissionCommand);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
        #endregion

        #region DELETE
        public async Task<ActionResult<bool>> Delete(DeletePermissionCommand deletePermissionCommand)
        {
            try
            {
                bool success = await this.mediator.Send(deletePermissionCommand);
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
