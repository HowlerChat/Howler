// <copyright file="ChannelController.cs" company="Howler Team">
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
    using Howler.Services.InteractionServices;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides actions to retrieve and configure Channels.
    /// </summary>
    [ApiController]
    [Route("v1/spaces/{spaceId}/channels")]
    [Authorize]
    public class ChannelController : ControllerBase
    {
        private readonly ILogger<ChannelController> _logger;

        private readonly ISpaceInteractionService _spaceInteractionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelController"/>
        /// class.
        /// </summary>
        /// <param name="logger">An injected instance of the logger.</param>
        /// <param name="spaceInteractionService">
        /// An injected instance of the space interaction service.
        /// </param>
        public ChannelController(
            ILogger<ChannelController> logger,
            ISpaceInteractionService spaceInteractionService)
        {
            this._logger = logger;
            this._spaceInteractionService = spaceInteractionService;
        }

        /// <summary>
        /// Retrieves a list of channels for a given space.
        /// </summary>
        /// <param name="spaceId">A space identifier.</param>
        /// <returns>A list of channels.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChannelInfoResponse>), 200)]
        public ActionResult GetChannelList(string spaceId)
        {
            var channels = this._spaceInteractionService
                .GetChannelsForSpace(spaceId);

            return this.Ok();
        }
    }
}