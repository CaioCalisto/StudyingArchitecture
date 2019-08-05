using System.Collections.Generic;
using System.Runtime.Serialization;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Domain.Aggregate
{
    public class Permission: Entity, IAggregateRoot
    {
        public int PermissionId { get; private set; }
        public string Name { get; private set; }

        [IgnoreDataMember]
        public ICollection<RolePermission> RolePermissions { get; private set; }

        public static Permission Create(string name)
        {
            return new Permission()
            {
                Name = name
            };
        }

    }
}
