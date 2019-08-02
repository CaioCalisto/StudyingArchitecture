using MediatR;

namespace UserAuthentication.Application.Commands.Users
{
    public class UpdateUserCommand: IRequest<bool>
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string AccessKey { get; set; }
    }
}
