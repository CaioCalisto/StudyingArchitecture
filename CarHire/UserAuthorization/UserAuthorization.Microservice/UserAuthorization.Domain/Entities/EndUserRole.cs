using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;

namespace UserAuthorization.Domain.Entities
{
    public class EndUserRole: Entity
    {
        public int EndUserId { get; private set; }
        public EndUser EndUser { get; private set; }
        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public static EndUserRole Create(int endUserId, int roleId)
        {
            return new EndUserRole()
            {
                EndUserId = endUserId,
                RoleId = roleId
            };
        }
    }
}
