// <copyright file="SpaceController.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Howler.Database.Core.Models;
    using Howler.Services.InteractionServices;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides actions to retrieve and configure Spaces.
    /// </summary>
    [ApiController]
    [Route("v1/spaces")]
    [Authorize]
    public class SpaceController : ControllerBase
    {
        private readonly ILogger<SpaceController> _logger;

        private readonly ISpaceInteractionService _spaceInteractionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceController"/>
        /// class.
        /// </summary>
        /// <param name="logger">An injected instance of a logger.</param>
        /// <param name="spaceInteractionService">
        /// An injected instance of the space interaction service.
        /// </param>
        public SpaceController(
            ILogger<SpaceController> logger,
            ISpaceInteractionService spaceInteractionService)
        {
            this._logger = logger;
            this._spaceInteractionService = spaceInteractionService;
        }

        /// <summary>
        /// Retrieves the space given a space id.
        /// </summary>
        /// <param name="spaceId">The space identifier hash.</param>
        /// <returns>A response containing space information.</returns>
        [HttpGet("{spaceId}")]
        [ProducesResponseType(typeof(SpaceResponse), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Get(string spaceId)
        {
            var space = this._spaceInteractionService
                .GetSpaceBySpaceId(spaceId);

            if (space != null)
            {
                return this.Ok(space);
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// Creates a space.
        /// </summary>
        /// <param name="request">The create request.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(SpaceResponse), 200)]
        [ProducesResponseType(
            typeof(ValidationErrorResponse),
            400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> CreateAsync(
            CreateOrUpdateSpaceRequest request)
        {
            var space = await this._spaceInteractionService
                .CreateSpaceAsync(request);

            if (space.Left != null)
            {
                return this.Ok(space.Left);
            }
            else
            {
                return this.BadRequest(space.Right!);
            }
        }

        /// <summary>
        /// Updates a space. Must be called by the space owner.
        /// </summary>
        /// <param name="spaceId">The space identifier.</param>
        /// <param name="request">The update request.</param>
        /// <returns>A response indicating success or failure.</returns>
        [HttpPut("{spaceId}")]
        [ProducesResponseType(typeof(SpaceResponse), 200)]
        [ProducesResponseType(typeof(ValidationErrorResponse), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Update(
            string spaceId,
            CreateOrUpdateSpaceRequest request)
        {
            if (spaceId != request.SpaceId)
            {
                return this.BadRequest(
                    new ValidationErrorResponse("spaceId", "INVALID_SPACE_ID"));
            }

            var space = this._spaceInteractionService.UpdateSpace(request);

            if (space.Left == null && space.Right == null)
            {
                return this.NotFound();
            }
            else if (space.Left != null)
            {
                return this.Ok(space.Left);
            }

            return this.BadRequest(space.Right);
        }

        /// <summary>
        /// Deletes a space. Must be called by the space owner.
        /// </summary>
        /// <param name="spaceId">The space identifier.</param>
        /// <returns>A status code indicating success or failure.</returns>
        [HttpDelete("{spaceId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSpace(string spaceId)
        {
            var deleteError = this._spaceInteractionService
                .DeleteSpaceBySpaceId(spaceId);

            if (deleteError != null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok();
            }
        }

        /// <summary>
        /// Adds a federated server to the Howler indexing service.
        /// </summary>
        /// <param name="serverUrl">The url of the server to index.</param>
        /// <returns>OK.</returns>
        [HttpPut("federation/index")]
        [ProducesResponseType(200)]
        public IActionResult AddFederatedServer(string serverUrl)
        {
            // Validate request object
            var uri = new Uri(serverUrl);
            var isUnsafeUri = uri.Scheme != "https" ||
            uri.Host.EndsWith(".howler.chat");

            // TODO: Validate /.well-known/howler.json

            // TODO: Pull through howler.json and deserialize it
            // and enumerate over its info

            // Pre-store it in our space table
            // Add federated server to auxillary federated servers list
            // to be indexed in the background
            return this.Ok();
        }
    }
}
