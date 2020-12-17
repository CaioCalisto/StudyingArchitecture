// <copyright file="PagedList.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos.Table;

namespace Contoso.Registration.Application.Model
{
    /// <summary>
    /// Paged list.
    /// </summary>
    /// <typeparam name="T">Type of object.</typeparam>
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
        /// </summary>
        /// <param name="items">Items.</param>
        /// <param name="page">Page number.</param>
        /// <param name="limit">Limit of result.</param>
        /// <param name="total">Total of result.</param>
        public PagedList(IEnumerable<T> items, int page, int limit, int total)
        {
            this.AddRange(items);
            this.Page = page;
            this.Limit = limit;
            this.Total = total;
        }

        /// <summary>
        /// Gets or sets Page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets Limit.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets. Total.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Create a page list object.
        /// </summary>
        /// <param name="source">Source data.</param>
        /// <param name="page">Page.</param>
        /// <param name="limit">Limit.</param>
        /// <returns>Results.</returns>
        public static PagedList<T> ToPagedList(IQueryable<TableEntityAdapter<T>> source, int page, int limit) =>
            new PagedList<T>(source.ToList().Skip((page - 1) * limit).Take(limit).Select(c => c.OriginalEntity), page, limit, source.ToList().Count());
    }
}
