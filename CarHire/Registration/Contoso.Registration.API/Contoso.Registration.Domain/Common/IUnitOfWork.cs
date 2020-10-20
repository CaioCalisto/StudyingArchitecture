// <copyright file="IUnitOfWork.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.Registration.Domain.Common
{
    /// <summary>
    /// Unit of Work interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Persist entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True of false.</returns>
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Begin transaction.
        /// </summary>
        /// <returns>Task.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commit transaction.
        /// </summary>
        /// <returns>Task.</returns>
        Task CommitTransactionAsync();

        /// <summary>
        /// Rollback transaction.
        /// </summary>
        void RollbackTransaction();
    }
}
