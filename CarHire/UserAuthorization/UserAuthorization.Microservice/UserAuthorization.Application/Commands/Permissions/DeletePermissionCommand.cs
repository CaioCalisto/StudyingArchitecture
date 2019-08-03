using MediatR;

namespace UserAuthorization.Application.Commands.Permissions
{
    public class DeletePermissionCommand: IRequest<bool>
    {
        public int PermissionId { get; set; }
    }
}
