using System.Collections.Generic;
using System.Runtime.Serialization;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.DomainEvents;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Aggregate
{
    public class Role : Entity, IAggregateRoot
    {
        public int RoleId { get; private set; }
        public string Name { get; private set; }

        [IgnoreDataMember]
        public ICollection<EndUserRole> EndUserRoles { get; private set; }

        [IgnoreDataMember]
        public ICollection<Permission> Permissions { get; private set; }

        [IgnoreDataMember]
        public int? SubDomainId { get; private set; }

        [IgnoreDataMember]
        public SubDomain SubDomain { get; private set; }

        public static Role Create(string name, int subDomainId)
        {
            return new Role()
            {
                Name = name,
                SubDomainId = subDomainId
            };
        }

        public void SetSubDomain(SubDomain subDomain)
        {
            this.SubDomain = subDomain;
        }

        public void DeleteRole()
        {
            this.AddDomainEvent(new EndUserOrRoleDeletedEvent()
            {
                RoleId = this.RoleId,
                EndUserId = 0
            });
        }
    }
}
