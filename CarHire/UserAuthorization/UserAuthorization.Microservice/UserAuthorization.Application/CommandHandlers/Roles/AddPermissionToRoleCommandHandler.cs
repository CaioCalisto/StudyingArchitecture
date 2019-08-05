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
    public class AddPermissionToRoleCommandHandler : IRequestHandler<AddPermissionToRoleCommand, bool>
    {
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionRepository permissionRepository;

        public AddPermissionToRoleCommandHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
        {
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
        }

        public async Task<bool> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = this.roleRepository.SelectRoleById(request.RoleId);
            Permission permission = permissionRepository.SelectByPermissionId(request.PermissionId);
            if (role != null && permission != null)
            {

                this.permissionRepository.Insert(RolePermission.Create(permission.PermissionId, role.RoleId));
                await this.permissionRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }

            return false;
        }
    }
}
