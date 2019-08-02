using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using UserAuthentication.Domain.Aggregates;
using UserAuthentication.Domain.Common;

namespace UserAuthentication.Infrastructure
{
    public class UserDBContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction transaction;

        public UserDBContext(DbContextOptions<UserDBContext> options, IMediator mediator)
            : base(options)
        {
            this._mediator = mediator;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("EndUser");
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._mediator.DispatchDomainEventsAsync(this);
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
