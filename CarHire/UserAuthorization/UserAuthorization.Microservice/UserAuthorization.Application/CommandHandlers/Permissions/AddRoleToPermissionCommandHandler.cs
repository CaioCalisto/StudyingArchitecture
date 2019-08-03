using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Permissions;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.Permissions
{
    public class AddRoleToPermissionCommandHandler : IRequestHandler<AddRoleToPermissionCommand, bool>
    {
        private readonly IRoleRepository roleRepository;

        public AddRoleToPermissionCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AddRoleToPermissionCommand request, CancellationToken cancellationToken)
        {
            Permission permission = this.roleRepository.SelectByPermissionId(request.PermissionId);
            Role role = this.roleRepository.SelectRoleById(request.RoleId);
            if (permission != null && role != null)
            {
                permission.SetRole(role);
                this.roleRepository.Update(permission);
                await this.roleRepository.UnitOfWork.SaveEntitiesAsync();

                return true;
            }

            return false;
        }
    }
}
