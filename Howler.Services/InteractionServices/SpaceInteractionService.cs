// <copyright file="SpaceInteractionService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.InteractionServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Howler.Database;
    using Howler.Database.Core;
    using Howler.Database.Core.Models;
    using Howler.Database.Indexer;
    using Howler.Database.Indexer.Models;
    using Howler.Services.Addressing;
    using Howler.Services.Authorization;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// An interaction service for managing spaces.
    /// </summary>
    public class SpaceInteractionService : ISpaceInteractionService
    {
        private ILogger<SpaceInteractionService> _logger;

        private IIndexerDatabaseContext _indexerDatabaseContext;

        private ICoreDatabaseContext _coreDatabaseContext;

        private IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SpaceInteractionService"/> class.
        /// </summary>
        /// <param name="logger">An injected logger instance.</param>
        /// <param name="indexerDatabaseContext">
        /// An injected federation database instance.
        /// </param>
        /// <param name="coreDatabaseContext">
        /// An injected core database instance.
        /// </param>
        /// <param name="authorizationService">
        /// An injected authorization service.
        /// </param>
        public SpaceInteractionService(
            ILogger<SpaceInteractionService> logger,
            IIndexerDatabaseContext indexerDatabaseContext,
            ICoreDatabaseContext coreDatabaseContext,
            IAuthorizationService authorizationService)
        {
            this._logger = logger;
            this._indexerDatabaseContext = indexerDatabaseContext;
            this._coreDatabaseContext = coreDatabaseContext;
            this._authorizationService = authorizationService;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// TODO: need to incorporate server registry.
        /// </remarks>
        public async Task<Models.Either<ErrorResponse, SpaceResponse>>
            CreateSpaceAsync(CreateOrUpdateSpaceRequest request)
        {
            if (!await this._authorizationService
                .IsAuthorizedAsync("create_space"))
            {
                return new Models.Either<ErrorResponse, SpaceResponse>(
                        new ValidationErrorResponse(
                            "authorization",
                            "INVALID_SCOPE"));
            }

            if (!string.IsNullOrEmpty(request.VanityUrl))
            {
                var vanityUrl = VanityUrl.Parse(request.VanityUrl).Map(url =>
                {
                    var vanity = this._indexerDatabaseContext.SpaceVanityUrls
                        .Where(
                            v => v.VanityUrl == request.VanityUrl.Trim()
                            .ToLowerInvariant()).ToList().FirstOrDefault();

                    if (vanity != null)
                    {
                        return new Models.Either<ErrorResponse, VanityUrl>(
                            new ValidationErrorResponse(
                                "vanityUrl",
                                "ALREADY_TAKEN"));
                    }

                    return new Models.Either<ErrorResponse, VanityUrl>(
                        url);
                });

                if (vanityUrl.Left != null)
                {
                    return new Models.Either<ErrorResponse, SpaceResponse>(
                        vanityUrl.Left);
                }
            }

            if (Guid.TryParse(request.SpaceId, out Guid spaceId) &&
                !this._coreDatabaseContext.Spaces.Where(
                    s => s.SpaceId == request.SpaceId.ToLowerInvariant())
                    .ToList().Any())
            {
                var space = new Space
                {
                    SpaceId = request.SpaceId,
                    SpaceName = request.SpaceName,
                    Description = request.Description,
                    VanityUrl = request.VanityUrl?.Trim().ToLowerInvariant(),
                    ServerUrl = "https://api.howler.chat",
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                    DefaultChannelId = request.SpaceId,
                    UserId = this._authorizationService.User!.Subject,
                    ChannelGroups = Encoding.UTF8.GetBytes(
                        JsonConvert.SerializeObject(
                            new ChannelGroupResponse
                            {
                                { "General", new string[] { request.SpaceId } },
                            })),
                };

                this._coreDatabaseContext.Spaces.Add<Space, string>(space);

                if (request.VanityUrl != null)
                {
                    var vanity = new SpaceVanityUrl
                    {
                        VanityUrl = request.VanityUrl?.Trim()
                            .ToLowerInvariant(),
                        SpaceId = request.SpaceId,
                    };
                    this._indexerDatabaseContext.SpaceVanityUrls
                        .Add<SpaceVanityUrl, string>(vanity);
                }

                this._coreDatabaseContext.Channels
                    .Add<Channel, Tuple<string, string>>(new Channel
                {
                    ChannelId = request.SpaceId,
                    SpaceId = request.SpaceId,
                    ChannelName = "general",
                    MemberId = this._authorizationService.User!.Subject,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow,
                });

                return new Models.Either<ErrorResponse, SpaceResponse>(
                        new SpaceResponse(space));
            }
            else
            {
                return new Models.Either<ErrorResponse, SpaceResponse>(
                        new ValidationErrorResponse(
                            "spaceId",
                            "INVALID_SPACE_ID"));
            }
        }

        /// <inheritdoc/>
        public ErrorResponse? DeleteSpaceBySpaceId(string spaceId)
        {
            return new ValidationErrorResponse("spaceId", "INVALID_SPACE_ID");
        }

        /// <inheritdoc/>
        public SpaceResponse? GetSpaceBySpaceId(string spaceId)
        {
            var space = this._coreDatabaseContext.Spaces.Where(
                s => s.SpaceId == spaceId.ToLower())
                .ToList().FirstOrDefault();

            if (space != null)
            {
                return new SpaceResponse(space);
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public Models.Either<ErrorResponse, SpaceResponse?> UpdateSpace(
            CreateOrUpdateSpaceRequest request)
        {
            if (request.SpaceId != null)
            {
                var space = this._coreDatabaseContext.Spaces.Where(
                    s => s.SpaceId == request.SpaceId.ToLower())
                    .ToList().FirstOrDefault();

                try
                {
                    if (!string.IsNullOrEmpty(request.VanityUrl))
                    {
                        var uri = new Uri(request.VanityUrl);
                        if (
                            uri.Host != "howler.gg" ||
                            uri.Scheme != "https" ||
                            !uri.IsDefaultPort)
                        {
                            return new Models
                                .Either<ErrorResponse, SpaceResponse?>(
                                new ValidationErrorResponse(
                                    "vanityUrl",
                                    "INVALID_URL"));
                        }
                    }
                }
                catch (UriFormatException)
                {
                    return new Models.Either<ErrorResponse, SpaceResponse?>(
                        new ValidationErrorResponse(
                            "vanityUrl",
                            "INVALID_URL"));
                }

                if (space != null)
                {
                    if (this._coreDatabaseContext.Channels.Where(
                        c => c.ChannelId == request.DefaultChannelId &&
                            c.SpaceId == space.SpaceId).ToList()
                            .FirstOrDefault() == null)
                    {
                        return new Models.Either<ErrorResponse, SpaceResponse?>(
                            new ValidationErrorResponse(
                                "defaultChannelId",
                                "INVALID_ID"));
                    }

                    if (request.VanityUrl?.Trim().ToLowerInvariant() !=
                        space.VanityUrl)
                    {
                        if (space.VanityUrl == null)
                        {
                            var vanity = new SpaceVanityUrl
                            {
                                VanityUrl = request.VanityUrl?.Trim()
                                    .ToLowerInvariant(),
                                SpaceId = request.SpaceId,
                            };
                            this._indexerDatabaseContext.SpaceVanityUrls
                                .Add<SpaceVanityUrl, string>(vanity);
                        }
                        else if (string.IsNullOrWhiteSpace(request.VanityUrl))
                        {
                            var vanity = this._indexerDatabaseContext
                                .SpaceVanityUrls.Where(
                                    v => v.VanityUrl == space.VanityUrl)
                                .ToList().FirstOrDefault();

                            if (vanity != null)
                            {
                                this._indexerDatabaseContext.SpaceVanityUrls
                                    .Remove<SpaceVanityUrl, string>(vanity);
                            }
                        }
                        else
                        {
                            var vanity = this._indexerDatabaseContext
                                .SpaceVanityUrls.Where(
                                    v => v.VanityUrl == space.VanityUrl)
                                .ToList().FirstOrDefault();

                            if (vanity != null)
                            {
                                this._indexerDatabaseContext.SpaceVanityUrls
                                    .Remove<SpaceVanityUrl, string>(vanity);
                            }

                            vanity = new SpaceVanityUrl
                            {
                                VanityUrl = request.VanityUrl?.Trim()
                                    .ToLowerInvariant(),
                                SpaceId = request.SpaceId,
                            };
                            this._indexerDatabaseContext.SpaceVanityUrls
                                .Add<SpaceVanityUrl, string>(vanity);
                        }
                    }

                    space.VanityUrl = request.VanityUrl?.Trim()
                        .ToLowerInvariant() ?? space.VanityUrl;
                    space.SpaceName = request.SpaceName ??
                        space.SpaceName;
                    space.Description = request.Description ??
                        request.Description;
                    space.ServerUrl = "https://api.howler.chat";
                    space.ModifiedDate = DateTime.UtcNow;
                    space.DefaultChannelId = request.DefaultChannelId ??
                        space.DefaultChannelId;

                    this._coreDatabaseContext.Spaces
                        .Update<Space, string>(space);
                    return new Models.Either<ErrorResponse, SpaceResponse?>(
                        new SpaceResponse(space));
                }
            }

            return new Models.Either<ErrorResponse, SpaceResponse?>(
                (SpaceResponse?)null);
        }

#pragma warning disable SA1615
        /// <summary>
        /// Adds or updates a message id as the unread status of a channel
        /// member state.
        /// </summary>
        /// <param name="channelId">The channelId to set.</param>
        /// <param name="messageId">The messageId to set.</param>
        public async Task AddUnreadChannelMemberState(
            string channelId,
            string messageId)
        {
            await Task.Run(() =>
            {
                var channelMembers = this._coreDatabaseContext.ChannelMembers
                    .Where(cm => cm.ChannelId == channelId)
                    .ToList();

                // TODO: Bulk insert
                foreach (var channelMember in channelMembers)
                {
                    this._coreDatabaseContext.ChannelMemberStates.Add<
                        ChannelMemberState,
                        Tuple<string, string, string>>(
                        new ChannelMemberState
                        {
                        });
                }
            });
        }
#pragma warning restore SA1615
    }
}
