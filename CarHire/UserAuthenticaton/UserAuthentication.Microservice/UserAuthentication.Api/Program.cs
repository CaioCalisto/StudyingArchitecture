using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using UserAuthentication.Infrastructure;

namespace UserAuthentication.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    UserDBContext context = services.GetRequiredService<UserDBContext>();
                    context.Database.EnsureCreated();
                    CreateAdminIfNotExists(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void CreateAdminIfNotExists(UserDBContext context)
        {
            if (context.Users.Where(u => u.UserName == "admin").FirstOrDefault() == null)
            {
                context.Users.Add(Domain.Aggregates.User
                    .Create("admin", "admin123"));
            }
        }
    }
}
