using MediatR;

namespace UserAuthorization.Application.Commands.Roles
{
    public class AddPermissionToRoleCommand: IRequest<bool>
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }
    }
}
