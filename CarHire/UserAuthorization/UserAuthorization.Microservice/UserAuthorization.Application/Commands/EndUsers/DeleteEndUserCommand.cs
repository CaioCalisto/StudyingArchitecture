using MediatR;

namespace UserAuthorization.Application.Commands.EndUsers
{
    public class DeleteEndUserCommand: IRequest<bool>
    {
        public int EndUserId { get; set; }
    }
}
