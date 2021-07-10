// <copyright file="FederationInteractionService.cs" company="Howler Team">
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
    using System.Threading.Tasks;
    using Howler.Database;
    using Howler.Database.Models;
    using Howler.Services.Authorization;
    using Howler.Services.Models.V1.Federation;
    using Howler.Services.Models.V1.Space;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// An interaction service for managing federation info.
    /// </summary>
    public class FederationInteractionService : IFederationInteractionService
    {
        private ILogger<SpaceInteractionService> _logger;

        private IFederationDatabaseContext _federationDatabaseContext;

        private IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FederationInteractionService"/> class.
        /// </summary>
        /// <param name="logger">An injected logger instance.</param>
        /// <param name="federationDatabaseContext">
        /// An injected federation database instance.
        /// </param>
        /// <param name="authorizationService">
        /// An injected authorization service.
        /// </param>
        public FederationInteractionService(
            ILogger<SpaceInteractionService> logger,
            IFederationDatabaseContext federationDatabaseContext,
            IAuthorizationService authorizationService)
        {
            this._logger = logger;
            this._federationDatabaseContext = federationDatabaseContext;
            this._authorizationService = authorizationService;
        }

        /// <inheritdoc/>
        public async Task<UserSpacesResponse?> GetUserSpaces()
        {
            if (await this._authorizationService.IsAuthorizedAsync(
                "get_user_spaces"))
            {
                if (this._authorizationService.User != null)
                {
                    var userSpaces = this._federationDatabaseContext.UserSpaces
                        .Where(us => us.UserId ==
                            this._authorizationService.User.Subject.ToLower())
                        .ToList().FirstOrDefault();

                    if (userSpaces != null)
                    {
                        return new UserSpacesResponse(userSpaces);
                    }
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public async Task JoinSpace(string spaceId)
        {
            if (await this._authorizationService.IsAuthorizedAsync(
                "join_spaces"))
            {
                if (this._authorizationService.User != null)
                {
                    var userSpaces = this._federationDatabaseContext.UserSpaces
                        .Where(us => us.UserId ==
                            this._authorizationService.User.Subject.ToLower())
                        .ToList().FirstOrDefault();

                    if (Guid.TryParse(spaceId, out Guid spaceGuid))
                    {
                        if (userSpaces != null)
                        {
                            if (!userSpaces.SpaceIds!
                                .Contains(spaceId.ToLower()))
                            {
                                userSpaces.SpaceIds!.Append(
                                    spaceId.ToLower());
                            }

                            this._federationDatabaseContext.UserSpaces
                                .Update<UserSpaces, string>(userSpaces);
                        }
                        else
                        {
                            userSpaces = new Database.Models.UserSpaces
                            {
                                UserId = this._authorizationService.User
                                    .Subject.ToLower(),
                                SpaceIds = new List<string>
                                {
                                    spaceId.ToLower(),
                                },
                            };

                            this._federationDatabaseContext.UserSpaces
                                .Add<UserSpaces, string>(userSpaces);
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task<SpaceResponse?> FindSpaceByUrl(string url)
        {
            if (await this._authorizationService.IsAuthorizedAsync(
                "find_space_by_url"))
            {
                if (this._authorizationService.User != null)
                {
                    var spaceVanityUrl =
                        this._federationDatabaseContext.SpaceVanityUrls
                        .Where(
                            s => s.VanityUrl == url.ToLowerInvariant())
                        .ToList().FirstOrDefault();

                    if (spaceVanityUrl != null)
                    {
                        // TODO: when federation support is live this will
                        // need tweaking, sort of.
                        var space = this._federationDatabaseContext.Spaces
                            .Where(s => s.SpaceId == spaceVanityUrl.SpaceId)
                            .ToList().First();
                        return new SpaceResponse(space);
                    }
                }
            }

            return null;
        }
    }
}