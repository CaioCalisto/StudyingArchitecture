// <copyright file="VehicleContext.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contoso.Registration.Domain.Aggregate;
using Contoso.Registration.Domain.Ports;
using Contoso.Registration.Infrastructure.Configurations;
using MediatR;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace Contoso.Registration.Infrastructure.Database.AzureTableStorage
{
    /// <summary>
    /// Vehicle database context.
    /// </summary>
    public class VehicleContext : IVehicleContext, IDatabaseQueries
    {
        private readonly CloudTable table;
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleContext"/> class.
        /// </summary>
        /// <param name="tableStorageConfig">Table Storage onfigurations.</param>
        /// <param name="mediator">Mediator.</param>
        public VehicleContext(IOptions<TableStorage> tableStorageConfig, IMediator mediator)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(tableStorageConfig.Value.ConnectionString);
            CloudTableClient tableClient = cloudStorageAccount.CreateCloudTableClient();
            this.table = tableClient.GetTableReference(tableStorageConfig.Value.Table);
            this.table.CreateIfNotExists();
            this.mediator = mediator;
        }

        /// <inheritdoc/>
        public IQueryable<TableEntityAdapter<T>> GetQuery<T>()
        {
            return this.table.CreateQuery<TableEntityAdapter<T>>().AsQueryable();
        }

        /// <inheritdoc/>
        public async Task<Domain.Aggregate.Vehicle> InsertAsync(Domain.Aggregate.Vehicle entity, string partitionKey, string rowKey)
        {
            TableEntityAdapter<Domain.Aggregate.Vehicle> storageEntity = new TableEntityAdapter<Domain.Aggregate.Vehicle>(entity, partitionKey, rowKey);
            TableResult result = await this.table.ExecuteAsync(TableOperation.InsertOrMerge(storageEntity));
            await this.DispatchDomainEventsAsync(entity);
            return entity;
        }

        private async Task DispatchDomainEventsAsync(Vehicle root)
        {
            List<INotification> domainEvents = root.DomainEvents.ToList();
            foreach (var domainEvent in domainEvents)
            {
                await this.mediator.Publish(domainEvent);
            }

            domainEvents.ForEach(d => root.RemoveDomainEvent(d));
        }
    }
}
