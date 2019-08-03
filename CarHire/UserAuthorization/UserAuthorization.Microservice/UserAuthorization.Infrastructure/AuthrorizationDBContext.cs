using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UserAuthorization.Domain.Aggregate;
using UserAuthorization.Domain.Common;
using UserAuthorization.Domain.Entities;

namespace UserAuthorization.Infrastructure
{
    public class AuthorizationDBContext : DbContext, IUnitOfWork
    {
        private readonly IMediator mediator;
        private IDbContextTransaction transaction;

        public AuthorizationDBContext(DbContextOptions<AuthorizationDBContext> options, IMediator mediator)
            : base(options)
        {
            this.mediator = mediator;
        }

        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SubDomain> SubDomains { get; set; }
        public DbSet<EndUserRole> EndUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EndUserRole>()
                .HasKey(er => new { er.EndUserId, er.RoleId  });

            modelBuilder.Entity<EndUserRole>()  
                .HasOne<EndUser>(e => e.EndUser)    
                .WithMany(e => e.EndUserRoles)
                .HasForeignKey(e => e.EndUserId);

            modelBuilder.Entity<EndUserRole>()
                .HasOne<Role>(r => r.Role)
                .WithMany(r => r.EndUserRoles)
                .HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<SubDomain>()
                .HasMany(s => s.Roles)
                .WithOne(s => s.SubDomain);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this.mediator.DispatchDomainEventsAsync(this);
            int result = await base.SaveChangesAsync();
            if (result == 0)
                return false;
            else
                return true;
        }

        public async Task BeginTransactionAsync()
        {
            transaction = transaction ?? await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveEntitiesAsync();
                transaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                transaction?.Rollback();
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }
            }
        }
    }
}
