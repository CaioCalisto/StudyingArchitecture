using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.DomainEvents;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.DomainEventHandlers
{
    public class SubDomainDeletedEventHandler : INotificationHandler<SubDomainDeletedEvent>
    {
        private readonly IRoleRepository roleRepository;

        public SubDomainDeletedEventHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public Task Handle(SubDomainDeletedEvent notification, CancellationToken cancellationToken)
        {
            IEnumerable<Role> roles = this.roleRepository.SelectRolesBySubDomainId(notification.SubDomainId);
            foreach (Role role in roles)
            {
                role.DeleteRole();
                this.roleRepository.Remove(role);
            }

            return Task.CompletedTask;
        }
    }
}
