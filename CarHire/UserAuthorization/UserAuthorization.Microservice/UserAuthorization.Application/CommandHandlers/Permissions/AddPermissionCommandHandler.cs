using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Permissions;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.Permissions
{
    public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, Permission>
    {
        private readonly IPermissionRepository permissionRepository;

        public AddPermissionCommandHandler(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public async Task<Permission> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            Permission permission = this.permissionRepository.Insert(Permission.Create(request.Name));
            await this.permissionRepository.UnitOfWork.SaveEntitiesAsync();
            return permission;
        }
    }
}
