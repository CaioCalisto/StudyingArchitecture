using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Roles;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.Roles
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = this.roleRepository.SelectRoleById(request.RoleId);
            
            if (role != null)
            {
                role.DeleteRole();
                this.roleRepository.Remove(role);
                await this.roleRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }

            return false;
        }
    }
}
