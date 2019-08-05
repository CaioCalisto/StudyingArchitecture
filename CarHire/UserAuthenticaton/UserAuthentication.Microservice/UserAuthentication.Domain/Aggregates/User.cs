using UserAuthentication.Domain.Common;

namespace UserAuthentication.Domain.Aggregates
{
    public class User: IAggregateRoot
    {
        public int UserID { get; private set; }
        public string UserName { get; private set; }
        public string AccessKey { get; private set; }

        public static User Create(string userName, string accessKey)
        {
            return new User()
            {
                UserName = userName,
                AccessKey = accessKey
            };
        }

        public void SetUserName(string userName)
        {
            this.UserName = userName;
        }

        public void SetAccessKey(string accessKey)
        {
            this.AccessKey = accessKey;
        }
    }
}
