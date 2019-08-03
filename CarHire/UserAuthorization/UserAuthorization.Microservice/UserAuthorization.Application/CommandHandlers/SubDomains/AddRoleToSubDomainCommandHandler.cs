using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Application.Commands.SubDomains;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.CommandHandlers.SubDomains
{
    public class AddRoleToSubDomainCommandHandler : IRequestHandler<AddRoleToSubDomainCommand, bool>
    {
        private readonly ISubDomainRepository subDomainRepository;
        private readonly IRoleRepository roleRepository;

        public AddRoleToSubDomainCommandHandler(ISubDomainRepository subDomainRepository, IRoleRepository roleRepository)
        {
            this.subDomainRepository = subDomainRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AddRoleToSubDomainCommand request, CancellationToken cancellationToken)
        {
            Role role = this.roleRepository.SelectRoleById(request.RoleId);
            SubDomain subDomain = this.subDomainRepository.SelectById(request.SubDomainId);
            if (role != null && subDomain != null)
            {
                role.SetSubDomain(subDomain);
                this.roleRepository.Update(role);
                bool success = await this.roleRepository.UnitOfWork.SaveEntitiesAsync();
                return success;
            }

            return false;
        }
    }
}
