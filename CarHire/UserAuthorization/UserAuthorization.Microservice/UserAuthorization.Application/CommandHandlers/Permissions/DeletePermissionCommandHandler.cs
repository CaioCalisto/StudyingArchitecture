using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Permissions;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.Permissions
{
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
    {
        private readonly IRoleRepository roleRepository;

        public DeletePermissionCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            Permission permission = this.roleRepository.SelectByPermissionId(request.PermissionId);
            if (permission != null)
            {
                this.roleRepository.Remove(permission);
                await this.roleRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }

            return false;
        }
    }
}
