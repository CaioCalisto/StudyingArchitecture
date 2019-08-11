using MediatR;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Vehicle.Register.Domain.Common
{
    public abstract class Entity
    {
        private List<INotification> domainEvents;

        [IgnoreDataMember]
        public IEnumerable<INotification> DomainEvents
        {
            get { return this.domainEvents.AsReadOnly(); }
        }

        public Entity()
        {
            this.domainEvents = new List<INotification>();
        }

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }
    }
}
