using Customer.Register.Domain.Aggregate;
using Customer.Register.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Register.Infrastructure
{
    public class CustomerDBContext: DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private IDbContextTransaction transaction;

        public CustomerDBContext(DbContextOptions<CustomerDBContext> options, IMediator mediator)
            : base(options)
        {
            this._mediator = mediator;
        }

        public DbSet<Domain.Aggregate.Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Aggregate.Customer>()
                .ToTable("Customer")
                .HasOne(c => c.Address)
                .WithMany(a => a.Customers);
            
            modelBuilder.Entity<Address>()
                .ToTable("Address")
                .HasMany(a => a.Customers)
                .WithOne(c => c.Address);
            
            modelBuilder.Entity<County>()
                .ToTable("County")
                .HasMany(c => c.Addresses)
                .WithOne(a => a.County);

            modelBuilder.Entity<Country>()
                .ToTable("Country")
                .HasMany(c => c.Counties)
                .WithOne(c => c.Country);
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
