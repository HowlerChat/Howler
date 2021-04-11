namespace Howler.Services.Hubs
{
    using Howler.Services.InteractionServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [Authorize]
    public class HowlerHub : Hub
    {
        private ILogger<HowlerHub> _logger;

        private ISpaceInteractionService _spaceInteractionService;

        public HowlerHub(ILogger<HowlerHub> logger, ISpaceInteractionService spaceInteractionService)
        {
            this._logger = logger;
            this._spaceInteractionService = spaceInteractionService;
        }

        public async Task GetSpace(string spaceId)
        {
            var space = this._spaceInteractionService.GetSpaceBySpaceId(spaceId);

            if (space != null)
            {
                await Clients.Caller.SendAsync("GetSpaceResponse", space);
            }
            else
            {
                await Clients.Caller.SendAsync("NoSpaceFound");
            }
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}