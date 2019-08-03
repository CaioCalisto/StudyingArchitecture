using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Roles;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.Roles
{
    public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Role>
    {
        private readonly IRoleRepository roleRepository;

        public AddRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<Role> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = this.roleRepository.Insert(Role.Create(request.Name, request.SubDomainId));
            await this.roleRepository.UnitOfWork.SaveEntitiesAsync();
            return role;
        }
    }
}
