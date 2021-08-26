// <copyright file="IDatabaseQueries.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using Contoso.Registration.Infrastructure.Model;
using Microsoft.Azure.Cosmos.Table;

namespace Contoso.Registration.Infrastructure.Database.AzureTableStorage
{
    /// <summary>
    /// Database queries.
    /// </summary>
    public interface IDatabaseQueries
    {
        /// <summary>
        /// Get query.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <returns>Queryable.</returns>
        IQueryable<TableEntityAdapter<T>> GetQuery<T>();
    }
}
