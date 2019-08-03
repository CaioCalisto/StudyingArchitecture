using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.EndUsers;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.EndUsers
{
    public class DeleteEndUserCommandHandler : IRequestHandler<DeleteEndUserCommand, bool>
    {
        private readonly IEndUserRepository endUserRepository;

        public DeleteEndUserCommandHandler(IEndUserRepository endUserRepository)
        {
            this.endUserRepository = endUserRepository;
        }

        public async Task<bool> Handle(DeleteEndUserCommand request, CancellationToken cancellationToken)
        {
            EndUser endUser = this.endUserRepository.SelectEndUserById(request.EndUserId);
            if (endUser != null)
            {
                endUser.DeleteEndUser();
                this.endUserRepository.Remove(endUser);
                await this.endUserRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }

            return false;
        }
    }
}
