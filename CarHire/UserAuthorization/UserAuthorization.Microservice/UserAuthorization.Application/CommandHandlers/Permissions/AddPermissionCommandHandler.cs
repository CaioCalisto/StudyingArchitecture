using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.Permissions;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.Permissions
{
    public class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, Permission>
    {
        private readonly IRoleRepository roleRepository;

        public AddPermissionCommandHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public async Task<Permission> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
        {
            Permission permission = this.roleRepository.Insert(Permission.Create(request.Name));
            await this.roleRepository.UnitOfWork.SaveEntitiesAsync();
            return permission;
        }
    }
}
