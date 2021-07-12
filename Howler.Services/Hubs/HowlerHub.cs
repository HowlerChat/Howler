// <copyright file="HowlerHub.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Hubs
{
    using System.Threading.Tasks;
    using Howler.Services.InteractionServices;
    using Howler.Services.Models.V1.Space;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The main websocket handler for Howler client services.
    /// </summary>
    /// <note>Will probably get renamed to HowlerClientHub.</note>
    [Authorize]
    public class HowlerHub : Hub
    {
        private ILogger<HowlerHub> _logger;

        private ISpaceInteractionService _spaceInteractionService;

        private IFederationInteractionService _federationInteractionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HowlerHub"/> class.
        /// </summary>
        /// <param name="logger">The injected logger instance.</param>
        /// <param name="spaceInteractionService">
        /// The injected space interaction service instance.
        /// </param>
        /// <param name="federationInteractionService">
        /// The injected federation interaction service instance.
        /// </param>
        public HowlerHub(
            ILogger<HowlerHub> logger,
            ISpaceInteractionService spaceInteractionService,
            IFederationInteractionService federationInteractionService)
        {
            this._logger = logger;
            this._spaceInteractionService = spaceInteractionService;
            this._federationInteractionService = federationInteractionService;
        }

#pragma warning disable SA1615
        /// <summary>
        /// Requests space information. Sends a GetSpaceResponse if successful,
        /// otherwise sends a NoSpaceFound to the caller.
        /// </summary>
        /// <param name="spaceId">The space identifier.</param>
        public async Task GetSpace(string spaceId)
        {
            var space = this._spaceInteractionService
                .GetSpaceBySpaceId(spaceId);

            if (space != null)
            {
                await this.Clients.Caller.SendAsync("GetSpaceResponse", space);
            }
            else
            {
                await this.Clients.Caller.SendAsync("NoSpaceFound");
            }
        }

        /// <summary>
        /// Requests space information. Sends a GetSpaceResponse if successful,
        /// otherwise sends a NoSpaceFound to the caller.
        /// </summary>
        public async Task GetUserSpaces()
        {
            var userSpaces = await this._federationInteractionService
                .GetUserSpaces();

            if (userSpaces != null)
            {
                await this.Clients.Caller.SendAsync(
                    "GetUserSpacesResponse",
                    userSpaces);
            }
            else
            {
                await this.Clients.Caller.SendAsync("NoUserSpacesFound");
            }
        }

        /// <summary>
        /// Finds a space.
        /// </summary>
        /// <param name="url">The url for the space.</param>
        public async Task GetSpaceByUrl(string url)
        {
            var space =
                await this._federationInteractionService.FindSpaceByUrl(url);

            await this.Clients.Caller.SendAsync(
                "GetSpaceByUrlResponse",
                space);
        }

        /// <summary>
        /// Joins a space.
        /// </summary>
        /// <param name="spaceId">The space to join.</param>
        public async Task JoinSpace(string spaceId)
        {
            await this._federationInteractionService.JoinSpace(spaceId);
        }
#pragma warning restore SA1615
    }
}