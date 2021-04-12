namespace Howler.Services.Hubs
{
    using Howler.Services.InteractionServices;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    /// <summary>
    /// The main websocket handler for Howler client services.
    /// </summary>
    /// <note>Will probably get renamed to HowlerClientHub.</note>
    [Authorize]
    public class HowlerHub : Hub
    {
        private ILogger<HowlerHub> _logger;

        private ISpaceInteractionService _spaceInteractionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HowlerHub"/> class.
        /// </summary>
        /// <param name="logger">The injected logger instance.</param>
        /// <param name="spaceInteractionService">
        /// The injected space interaction service instance.
        /// </param>
        public HowlerHub(ILogger<HowlerHub> logger, ISpaceInteractionService spaceInteractionService)
        {
            this._logger = logger;
            this._spaceInteractionService = spaceInteractionService;
        }

#pragma warning disable SA1615
        /// <summary>
        /// Requests space information. Sends a GetSpaceResponse if successful,
        /// otherwise sends a NoSpaceFound to the caller.
        /// </summary>
        /// <param name="spaceId">The space identifier.</param>
        public async Task GetSpace(string spaceId)
        {
            var space = this._spaceInteractionService.GetSpaceBySpaceId(spaceId);

            if (space != null)
            {
                await this.Clients.Caller.SendAsync("GetSpaceResponse", space);
            }
            else
            {
                await this.Clients.Caller.SendAsync("NoSpaceFound");
            }
        }
#pragma warning restore SA1615
    }
}