using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserAuthorization.Application.Commands.Permissions
{
    public class AddRoleToPermissionCommand: IRequest<bool>
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
