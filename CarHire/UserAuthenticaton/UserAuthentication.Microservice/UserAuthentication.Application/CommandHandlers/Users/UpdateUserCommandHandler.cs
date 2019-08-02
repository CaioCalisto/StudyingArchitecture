using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthentication.Application.Commands.Users;
using UserAuthentication.Domain.Aggregates;
using UserAuthentication.Domain.Repositories;

namespace UserAuthentication.Application.CommandHandlers.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = this.userRepository.SelectById(request.UserID);
            user.SetAccessKey(request.AccessKey);
            user.SetUserName(request.UserName);
            this.userRepository.Update(user);
            await this.userRepository.UnitOfWork.SaveEntitiesAsync();
            return true;
        }
    }
}
