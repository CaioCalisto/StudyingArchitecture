using System;
using System.Linq;
using UserAuthentication.Domain.Aggregates;
using UserAuthentication.Domain.Common;
using UserAuthentication.Domain.Repositories;

namespace UserAuthentication.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext userDBContext;
        public IUnitOfWork UnitOfWork
        {
            get { return this.userDBContext; }
        }

        public UserRepository(UserDBContext userDBContext)
        {
            this.userDBContext = userDBContext ?? throw new ArgumentNullException(nameof(userDBContext));
        }

        public User SelectById(int userId)
        {
            return this.userDBContext
                   .Users
                   .Where(u => u.UserID == userId)
                   .FirstOrDefault();
        }

        public User Insert(User user)
        {
            return this.userDBContext
                .Add(user)
                .Entity;
        }

        public void Update(User user)
        {
            this.userDBContext
                .Update(user);
        }
    }
}
