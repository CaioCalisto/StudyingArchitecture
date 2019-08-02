using System.Threading.Tasks;
using UserAuthentication.Domain.Aggregates;

namespace UserAuthentication.Application.Services
{
    public interface IUserService
    {
        Task<User> FindUserAsync(int userId);
        Task<User> FindUserAsync(string userName);
    }
}