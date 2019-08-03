using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.SubDomains;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.SubDomains
{
    public class AddSubDomainCommandHandler : IRequestHandler<AddSubDomainCommand, SubDomain>
    {
        private readonly ISubDomainRepository subDomainRepository;

        public AddSubDomainCommandHandler(ISubDomainRepository subDomainRepository)
        {
            this.subDomainRepository = subDomainRepository;
        }

        public async Task<SubDomain> Handle(AddSubDomainCommand request, CancellationToken cancellationToken)
        {
            SubDomain subDomain = SubDomain.Create(request.Name, request.Code);
            subDomain = this.subDomainRepository.Insert(subDomain);
            await this.subDomainRepository.UnitOfWork.SaveEntitiesAsync();
            return subDomain;
        }
    }
}
