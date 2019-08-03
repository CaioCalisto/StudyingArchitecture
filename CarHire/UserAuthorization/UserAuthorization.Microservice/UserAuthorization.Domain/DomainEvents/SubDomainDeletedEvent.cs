using MediatR;

namespace UserAuthorization.Domain.DomainEvents
{
    public class SubDomainDeletedEvent: INotification
    {
        public int SubDomainId { get; set; }
    }
}
