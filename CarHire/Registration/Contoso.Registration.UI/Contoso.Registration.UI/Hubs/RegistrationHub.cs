using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Contoso.Registration.UI.Hubs
{
    public class RegistrationHub : Hub
    {
        public const string HubUrl = "/notifications";

        public async Task Broadcast(string message)
        {
            Console.WriteLine("Server sending message to clients");
            await Clients.All.SendAsync("Broadcast", message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Server Hub {Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}