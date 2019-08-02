using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UserAuthentication.Application.Commands.Users;
using UserAuthentication.Application.Services;
using UserAuthentication.Domain.Aggregates;

namespace UserAuthentication.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IUserService userService;

        public UserController(IMediator mediator, IUserService userService)
        {
            this.mediator = mediator;
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            try
            {
                return this.Ok(this.userService.FindUserAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(AddUserCommand addUserCommand)
        {
            try
            {
                Domain.Aggregates.User user = await this.mediator.Send(addUserCommand);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            try
            {
                bool success = await this.mediator.Send(updateUserCommand);
                return Ok(success);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
