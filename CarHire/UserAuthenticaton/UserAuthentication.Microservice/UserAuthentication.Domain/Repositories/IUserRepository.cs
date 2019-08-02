using UserAuthentication.Domain.Aggregates;
using UserAuthentication.Domain.Common;

namespace UserAuthentication.Domain.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        User SelectById(int userId);
        User Insert(User user);
        void Update(User user);
    }
}
