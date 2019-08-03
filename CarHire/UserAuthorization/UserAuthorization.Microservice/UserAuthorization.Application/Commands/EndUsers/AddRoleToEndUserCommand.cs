using MediatR;

namespace UserAuthorization.Application.Commands.EndUsers
{
    public class AddRoleToEndUserCommand: IRequest<bool>
    {
        public int EndUserId { get; set; }
        public int RoleId { get; set; }
    }
}
