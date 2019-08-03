using MediatR;

namespace UserAuthorization.Domain.DomainEvents
{
    public class EndUserOrRoleDeletedEvent: INotification
    {
        public int EndUserId { get; set; }
        public int RoleId { get; set; }
    }
}
