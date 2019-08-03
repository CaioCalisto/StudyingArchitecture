using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Domain.DomainEvents;
using UserAuthorization.Domain.Entities;
using UserAuthorization.Domain.Repositories;

namespace UserAuthorization.Application.DomainEventHandlers
{
    public class EndUserOrRoleDeletedEventHandler : INotificationHandler<EndUserOrRoleDeletedEvent>
    {
        private readonly IEndUserRepository endUserRepository;

        public EndUserOrRoleDeletedEventHandler(IEndUserRepository endUserRepository)
        {
            this.endUserRepository = endUserRepository;
        }

        public Task Handle(EndUserOrRoleDeletedEvent notification, CancellationToken cancellationToken)
        {
            IEnumerable<EndUserRole> endUserRoles = null;
            if (notification.RoleId != 0)
                endUserRoles = this.endUserRepository.SelectEndUserRoleByRoleId(notification.RoleId);
            else
                endUserRoles = this.endUserRepository.SelectEndUserRoleByEndUserId(notification.EndUserId);

            foreach (EndUserRole endUserRole in endUserRoles)
            {
                this.endUserRepository.Remove(endUserRole);
            }

            return Task.CompletedTask;
        }
    }
}
