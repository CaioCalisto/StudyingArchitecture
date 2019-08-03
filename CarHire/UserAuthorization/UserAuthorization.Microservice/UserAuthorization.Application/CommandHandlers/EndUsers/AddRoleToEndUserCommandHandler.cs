using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.EndUsers;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.EndUsers
{
    class AddRoleToEndUserCommandHandler : IRequestHandler<AddRoleToEndUserCommand, bool>
    {
        private readonly IEndUserRepository endUserRepository;
        private readonly IRoleRepository roleRepository;
        public AddRoleToEndUserCommandHandler(IEndUserRepository endUserRepository, IRoleRepository roleRepository)
        {
            this.endUserRepository = endUserRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AddRoleToEndUserCommand request, CancellationToken cancellationToken)
        {
            EndUser endUser = this.endUserRepository.SelectEndUserById(request.EndUserId);
            Role role = this.roleRepository.SelectRoleById(request.RoleId);
            if (endUser != null && role != null)
            {
                EndUserRole endUserRole = this.endUserRepository.Insert(
                    EndUserRole.Create(endUser.EndUserId, role.RoleId));

                bool success = await this.endUserRepository.UnitOfWork.SaveEntitiesAsync();
                return success;
            }

            return false;
        }
    }
}
