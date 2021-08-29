using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Contoso.Registration.UI.Hubs
{
    public class RegistrationHub : Hub
    {
        public const string HubUrl = "/notifications";

        public async Task Broadcast(string notification)
        {
            Console.WriteLine("Server sending notification to all clients");
            await Clients.All.SendAsync("Broadcast", notification);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"Server Hub {Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Server hub disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}