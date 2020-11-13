// <copyright file="VehicleContext.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contoso.Registration.Domain.Ports;
using Contoso.Registration.Infrastructure.Configurations;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace Contoso.Registration.Infrastructure.Database
{
    /// <summary>
    /// Vehicle database context.
    /// </summary>
    public class VehicleContext : IVehicleRepository, IDatabaseQueries
    {
        private readonly CloudTable table;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleContext"/> class.
        /// </summary>
        /// <param name="config">Configurations.</param>
        /// <param name="mapper">Auto mapper.</param>
        public VehicleContext(IOptions<TableStorageConfig> config, IMapper mapper)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(config.Value.ConnectionString);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            this.table = tableClient.GetTableReference(config.Value.Table);
            this.table.CreateIfNotExists();
            this.mapper = mapper;
        }

        /// <inheritdoc/>
        public IQueryable<TableEntityAdapter<T>> GetQuery<T>()
        {
            return this.table.CreateQuery<TableEntityAdapter<T>>().AsQueryable();
        }

        /// <inheritdoc/>
        public async Task<T> InsertAsync<T>(T entity, string partitionKey, string rowKey)
        {
            TableEntityAdapter<T> storageEntity = new TableEntityAdapter<T>(entity, partitionKey, rowKey);
            TableResult result = await this.table.ExecuteAsync(TableOperation.InsertOrMerge(storageEntity));
            return entity;
        }
    }
}
