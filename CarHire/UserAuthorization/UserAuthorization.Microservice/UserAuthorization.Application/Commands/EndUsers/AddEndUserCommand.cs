using MediatR;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Application.Commands.EndUsers
{
    public class AddEndUserCommand: IRequest<EndUser>
    {
        public string UserName { get; set; }
    }
}
