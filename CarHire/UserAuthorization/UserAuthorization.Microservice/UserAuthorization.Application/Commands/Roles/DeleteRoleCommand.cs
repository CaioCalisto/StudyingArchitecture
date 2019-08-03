using MediatR;

namespace UserAuthorization.Application.Commands.Roles
{
    public class DeleteRoleCommand: IRequest<bool>
    {
        public int RoleId { get; set; }
    }
}
