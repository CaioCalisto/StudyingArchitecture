using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Registration.Infrastructure.Messaging;

namespace Contoso.Registration.FunctionalTest.Stubs
{
    internal class MessageBusStub : IMessageBus
    {
        private List<IntegrationEvent> integrationEvents;

        public MessageBusStub()
        {
            this.integrationEvents = new List<IntegrationEvent>();
        }

        public IEnumerable<IntegrationEvent> IntegrationEvents
        {
            get { return this.integrationEvents.AsReadOnly(); }
        }

        public Task Publish(IntegrationEvent integrationEvent)
        {
            this.integrationEvents.Add(integrationEvent);
            return Task.CompletedTask;
        }

        public void Clear()
        {
            this.integrationEvents.Clear();
        }
    }
}
