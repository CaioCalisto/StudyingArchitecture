using System.Threading;
using System.Threading.Tasks;

namespace UserAuthorization.Domain.Common
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}
