using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.EndUsers;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.EndUsers
{
    public class AddEndUserCommandHandler : IRequestHandler<AddEndUserCommand, EndUser>
    {
        private readonly IEndUserRepository endUserRepository;

        public AddEndUserCommandHandler(IEndUserRepository endUserRepository)
        {
            this.endUserRepository = endUserRepository;
        }

        public async Task<EndUser> Handle(AddEndUserCommand request, CancellationToken cancellationToken)
        {
            EndUser endUser = this.endUserRepository.Insert(EndUser
                .Create(request.UserName));
            await this.endUserRepository.UnitOfWork.SaveEntitiesAsync();
            return endUser;
        }
    }
}
