using MediatR;

namespace UserAuthorization.Application.Commands.SubDomains
{
    public class AddRoleToSubDomainCommand: IRequest<bool>
    {
        public int SubDomainId { get; set; }
        public int RoleId { get; set; }
    }
}
