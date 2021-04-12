namespace Howler.Services.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Howler.Services.Models.V1.Space;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.InteractionServices;

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
            _logger = logger;
            _spaceInteractionService = spaceInteractionService;
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
            var space = this._spaceInteractionService.GetSpaceBySpaceId(spaceId);

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
        [ProducesResponseType(typeof(ValidationError), 400)]
        [ProducesResponseType(401)]
        public ActionResult Create(CreateOrUpdateSpaceRequest request)
        {
            var space = this._spaceInteractionService.CreateSpace(request);

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
        [ProducesResponseType(typeof(ValidationError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Update(
            string spaceId,
            CreateOrUpdateSpaceRequest request)
        {
            if (spaceId != request.SpaceId)
            {
                return this.BadRequest(
                    new ValidationError("spaceId", "INVALID_SPACE_ID"));
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
    }
}