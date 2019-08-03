using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.SubDomains;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.SubDomains
{
    public class DeleteSubDomainCommandHandler : IRequestHandler<DeleteSubDomainCommand, bool>
    {
        private readonly ISubDomainRepository subDomain;

        public DeleteSubDomainCommandHandler(ISubDomainRepository subDomain)
        {
            this.subDomain = subDomain;
        }

        public async Task<bool> Handle(DeleteSubDomainCommand request, CancellationToken cancellationToken)
        {
            SubDomain subDomain = this.subDomain.SelectById(request.SubDomainId);
            if (subDomain != null)
            {
                subDomain.Delete();
                this.subDomain.Delete(subDomain);
                await this.subDomain.UnitOfWork.SaveEntitiesAsync();
                return true;
            }

            return false;
        }
    }
}
