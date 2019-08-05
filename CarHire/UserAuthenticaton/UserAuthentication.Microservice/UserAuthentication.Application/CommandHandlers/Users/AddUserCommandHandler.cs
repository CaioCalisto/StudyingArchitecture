using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthentication.Application.Commands.Users;
using UserAuthentication.Domain.Aggregates;
using UserAuthentication.Domain.Repositories;

namespace UserAuthentication.Application.CommandHandlers.Users
{
    class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User user = this.userRepository.Insert(User.Create(request.UserName, request.AccessKey));
            await this.userRepository.UnitOfWork.SaveEntitiesAsync();
            return user;
        }
    }
}
