// <copyright file="HowlerHub.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Howler.Database.Core.Models;
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

        private string _serverId;

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
            this._serverId = Environment
                .GetEnvironmentVariable("HOWLER_SCOPE") ??
                throw new InvalidOperationException(
                    "HOWLER_SCOPE is not defined.");
        }

#pragma warning disable SA1615
        /// <summary>
        /// Requests a fleet of spaces and a channel to subscribe to for
        /// initial connection.
        /// </summary>
        /// <param name="spaceIds">
        /// The collection of spaces to subscribe to.
        /// </param>
        /// <param name="channelSpaceId">
        /// The specific channel's space id.
        /// </param>
        /// <param name="channelId">
        /// The specific channel to subscribe to.
        /// </param>
        public async Task SubscribeToSpacesAndChannel(
            List<string> spaceIds,
            string channelSpaceId,
            string channelId)
        {
            // TODO: Add authorization checks to ensure user is able to access
            // these spaces and channel.
            foreach (var spaceId in spaceIds)
            {
                await this.Groups.AddToGroupAsync(
                    this.Context.ConnectionId,
                    $"howler://{this._serverId}/{spaceId}");
            }

            await this.Groups.AddToGroupAsync(
                this.Context.ConnectionId,
                $"howler://{this._serverId}/{channelSpaceId}/{channelId}");

            await this.Clients.Caller.SendAsync("SubscriptionSuccess");
        }

        /// <summary>
        /// Sends a message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public async Task SendMessage(Message message)
        {
            // TODO: Validate and Store it
            await this.Clients.Group(
                $"howler://{this._serverId}/{message.SpaceId}/" +
                message.ChannelId).SendAsync("ReceiveMessage", message);

            // TODO: Update the unread status of this channel for those users
            // this._spaceInteractionService.
            // TODO: Shuttle the message (if unencrypted) to the search index
            // TODO: Send this message to the notification service for all
        }

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
