using System.Security.Claims;
using System.Threading.Tasks;
using UserAuthenticaton.Web.Models;

namespace UserAuthenticaton.Web.Services
{
    public interface ILoginService
    {
        Task<AuthenticationResponse> LogIn(EndUser endUser);
    }
}