using MediatR;

namespace UserAuthorization.Application.Commands.SubDomains
{
    public class DeleteSubDomainCommand: IRequest<bool>
    {
        public int SubDomainId { get; set; }
    }
}
