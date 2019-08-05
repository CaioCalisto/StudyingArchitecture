using MediatR;
using UserAuthentication.Domain.Aggregates;

namespace UserAuthentication.Application.Commands.Users
{
    public class AddUserCommand: IRequest<User>
    {
        public string UserName { get; set; }
        public string AccessKey { get; set; }
    }
}
