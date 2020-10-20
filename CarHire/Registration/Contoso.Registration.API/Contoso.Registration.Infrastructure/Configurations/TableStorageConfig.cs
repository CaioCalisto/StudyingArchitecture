// <copyright file="TableStorageConfig.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

namespace Contoso.Registration.Infrastructure.Configurations
{
    /// <summary>
    /// Table storage configurations.
    /// </summary>
    public class TableStorageConfig
    {
        /// <summary>
        /// Gets or Sets ConnectionString.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or Sets table name.
        /// </summary>
        public string Table { get; set; }
    }
}
