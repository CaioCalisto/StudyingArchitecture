// <copyright file="AzureBus.cs" company="CaioCesarCalisto">
// Copyright (c) CaioCesarCalisto. All rights reserved.
// </copyright>

using System;
using System.Text;
using System.Threading.Tasks;
using Contoso.Registration.Infrastructure.Configurations;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Contoso.Registration.Infrastructure.Messaging
{
    /// <summary>
    /// Azure Message Bus Service.
    /// </summary>
    public class AzureBus : IMessageBus
    {
        private const string INTEGRATIONEVENT = "IntegrationEvent";
        private readonly AzureMessaging azureMessaging;
        private ITopicClient topicClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureBus"/> class.
        /// </summary>
        /// <param name="azureMessage">Message config.</param>
        public AzureBus(IOptions<AzureMessaging> azureMessage)
        {
            this.azureMessaging = azureMessage.Value ?? throw new ArgumentNullException(nameof(AzureMessaging));
        }

        /// <inheritdoc/>
        public async Task PublishAsync(IntegrationEvent integrationEvent) =>
            await this.GetClient().SendAsync(new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(integrationEvent)),
                Label = INTEGRATIONEVENT,
            });

        private ITopicClient GetClient()
        {
            if (this.topicClient.IsClosedOrClosing)
            {
                this.topicClient = new TopicClient(
                    this.azureMessaging.ConnectionString,
                    this.topicClient.TopicName,
                    RetryPolicy.Default);
            }

            return this.topicClient;
        }
    }
}
