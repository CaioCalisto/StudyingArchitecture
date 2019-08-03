using System.Runtime.Serialization;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;

namespace UserAuthorization.Domain.Entities
{
    public class Permission: Entity
    {
        public int PermissionId { get; private set; }
        public string Name { get; private set; }

        [IgnoreDataMember]
        public int? RoleId { get; private set; }

        [IgnoreDataMember]
        public Role Role  { get; private set; }

        public static Permission Create(string name)
        {
            return new Permission()
            {
                Name = name
            };
        }

        public void SetRole(Role role)
        {
            this.Role = role;
        }
    }
}
