using Customer.Register.Application.HttpClients;
using Customer.Register.Web.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Customer.Register.Web.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection service, IConfiguration configuration)
        {
            IAuthorizationApi authorizationApi = service.BuildServiceProvider().GetService<IAuthorizationApi>();
            Task<IEnumerable<Application.Models.Role>> roleTask = Task.Run(() => authorizationApi.GetRolesBySubDomain(1, 0 , 20));
            roleTask.Wait();
            service.AddAuthorization(options =>
            {
                foreach(var role in roleTask.Result)
                {
                    Task<IEnumerable<Application.Models.Permission>> permissiontask = Task.Run(() => authorizationApi.GetPermissionsByRole(role.RoleId, 0, 20));
                    permissiontask.Wait();
                    foreach(var permission in permissiontask.Result)
                    {
                        options.AddPolicy(permission.Name, policy =>
                            policy.RequireRole(role.Name)    
                        );
                    }
                }
            });

            return service;
        }
    }
}
