using MediatR;
using UserAuthorization.Domain.Aggregate;

namespace UserAuthorization.Application.Commands.SubDomains
{
    public class AddSubDomainCommand: IRequest<SubDomain>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
