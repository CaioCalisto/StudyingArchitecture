using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.DomainEvents;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Aggregate
{
    public class EndUser: Entity, IAggregateRoot
    {
        public int EndUserId { get; private set; }
        public string UserName { get; private set; }

        [IgnoreDataMember]
        public ICollection<EndUserRole> EndUserRoles { get; private set; }

        public static EndUser Create(string userName)
        {
            return new EndUser()
            {
                UserName = userName
            };
        }

        public void DeleteEndUser()
        {
            this.AddDomainEvent(new EndUserOrRoleDeletedEvent()
            {
                EndUserId = this.EndUserId,
                RoleId = 0
            });
        }
    }
}
