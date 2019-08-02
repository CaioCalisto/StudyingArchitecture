using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using UserAuthentication.Api.Models;
using UserAuthentication.Application.Authentication;
using UserAuthentication.Application.Services;
using UserAuthentication.Domain.Aggregates;

namespace UserAuthentication.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{apiVersion}/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly SigningConfigurations signingConfigurations;
        private readonly TokenConfigurations tokenConfigurations;

        public LoginController(IUserService userService, 
            SigningConfigurations signingConfigurations, 
            TokenConfigurations tokenConfigurations)
        {
            this.userService = userService;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfigurations = tokenConfigurations;
        }

        [Authorize]
        [HttpGet]
        public ActionResult ValidateToken()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> Post([FromBody]Login login)
        {
            try
            {
                User user = await this.userService.FindUserAsync(login.UserName);
                bool validCredentials = await IsCredentialValid(login, user);
                if (validCredentials)
                {
                    return Ok(new AuthenticationResponse()
                    {
                        Authenticated = true,
                        Created = GetCreateDate(),
                        Expiration = GetExpiration(),
                        AccessToken = GenerateToken(GetClaimsIdentity(user)),
                        Message = "OK"
                    });
                }
                else
                {
                    return Ok(new AuthenticationResponse()
                    {
                        Authenticated = false,
                        Message = "Authentication failed"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            return new ClaimsIdentity(
                    new GenericIdentity(user.UserID.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserID.ToString())
                    }
                );
        }

        private string GenerateToken(ClaimsIdentity  identity)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = GetCreateDate(),
                Expires = GetExpiration()
            });

            return handler.WriteToken(securityToken);
        }

        private DateTime GetCreateDate()
        {
            return DateTime.Now;
        }

        private DateTime GetExpiration()
        {
            return DateTime.Now + TimeSpan.FromMinutes(tokenConfigurations.Minutes);
        }

        private async Task<bool> IsCredentialValid(Login login, User user)
        {
            if (user != null && user.UserID != 0)
            {
                return (login != null &&
                    user.UserName == login.UserName &&
                    user.AccessKey == login.AccessKey);
            }
            return false;
        }
    }
}