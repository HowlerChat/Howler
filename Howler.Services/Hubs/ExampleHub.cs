using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Howler.Services.Hubs
{
    public class ExampleHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}