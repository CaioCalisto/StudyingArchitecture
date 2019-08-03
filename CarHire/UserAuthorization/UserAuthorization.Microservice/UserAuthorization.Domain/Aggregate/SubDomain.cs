using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.DomainEvents;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Aggregate
{
    public class SubDomain : Entity, IAggregateRoot
    {
        public int SubDomainId { get; private set; }
        public string Name { get; private set; }
        public string Code { get; private set; }

        [IgnoreDataMember]
        public ICollection<Role> Roles { get; private set; }

        public static SubDomain Create(string name, string code)
        {
            return new SubDomain()
            {
                Name = name,
                Code = code
            };
        }

        public void Delete()
        {
            this.AddDomainEvent(new SubDomainDeletedEvent()
            {
                SubDomainId = this.SubDomainId
            });
        }
    }
}
