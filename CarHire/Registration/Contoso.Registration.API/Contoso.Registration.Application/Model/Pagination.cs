// <copyright file="Pagination.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

namespace Contoso.Registration.Application.Model
{
    /// <summary>
    /// Pagination parameters.
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Gets or sets Page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets Limit.
        /// </summary>
        public int Limit { get; set; }
    }
}
