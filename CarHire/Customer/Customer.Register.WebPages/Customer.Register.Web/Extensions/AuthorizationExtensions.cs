using Customer.Register.Web.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Register.Web.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.CUSTOMER_READ, policy =>
                    policy.RequireRole(Role.ADMIN, Role.MANAGER, Role.ANALYST));

                options.AddPolicy(Permissions.CUSTOMER_CREATE, policy =>
                    policy.RequireRole(Role.ADMIN, Role.MANAGER, Role.ANALYST));

                options.AddPolicy(Permissions.CUSTOMER_DELETE, policy =>
                    policy.RequireRole(Role.ADMIN, Role.MANAGER));

                options.AddPolicy(Permissions.CUSTOMER_UPDATE, policy =>
                    policy.RequireRole(Role.ADMIN, Role.MANAGER, Role.ANALYST));

                options.AddPolicy(Permissions.CUSTOMER_IDENTITY, policy =>
                    policy.RequireRole(Role.ADMIN, Role.MANAGER));
            });

            return service;
        }
    }
}
