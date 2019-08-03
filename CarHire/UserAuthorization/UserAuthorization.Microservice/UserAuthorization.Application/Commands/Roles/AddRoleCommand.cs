using MediatR;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Application.Commands.Roles
{
    public class AddRoleCommand: IRequest<Role>
    {
        public string Name { get; set; }
        public int SubDomainId { get; set; }
    }
}
