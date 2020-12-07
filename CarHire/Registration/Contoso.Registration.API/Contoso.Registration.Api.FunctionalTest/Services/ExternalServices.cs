using Contoso.Registration.Infrastructure.Messaging;

namespace Contoso.Registration.FunctionalTest.Services
{
    internal static class ExternalServices
    {
        public static IMessageBus MessageBus { get; set; }
    }
}
