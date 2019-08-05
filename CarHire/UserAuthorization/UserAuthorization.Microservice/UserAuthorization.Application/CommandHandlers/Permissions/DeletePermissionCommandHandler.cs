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
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, bool>
    {
        private readonly IPermissionRepository permissionRepository;

        public DeletePermissionCommandHandler(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            Permission permission = this.permissionRepository.SelectByPermissionId(request.PermissionId);
            if (permission != null)
            {
                this.permissionRepository.Remove(permission);
                await this.permissionRepository.UnitOfWork.SaveEntitiesAsync();
                return true;
            }

            return false;
        }
    }
}
