using MediatR;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Application.Commands.Permissions
{
    public class AddPermissionCommand: IRequest<Permission>
    {
        public string Name { get; set; }
    }
}
