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

    [ApiController]
    [Route("v1/spaces")]
    //[Authorize]
    public class SpaceController : ControllerBase
    {
        private readonly ILogger<SpaceController> _logger;

        private readonly ISpaceInteractionService _spaceInteractionService;

        public SpaceController(ILogger<SpaceController> logger, ISpaceInteractionService spaceInteractionService)
        {
            _logger = logger;
            _spaceInteractionService = spaceInteractionService;
        }

        /// <summary>
        /// Retrieves the space given a space id.
        /// </summary>
        /// <param name="spaceId">The space identifier hash</param>
        [HttpGet("{spaceId}")]
        [ProducesResponseType(typeof(SpaceResponse), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Get(string spaceId)
        {
            var space = this._spaceInteractionService.GetSpaceBySpaceId(spaceId);

            if (space != null)
            {
                return Ok(space);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(SpaceResponse), 200)]
        [ProducesResponseType(typeof(ValidationError), 400)]
        [ProducesResponseType(401)]
        public ActionResult Create(CreateOrUpdateSpaceRequest request)
        {
            var space = this._spaceInteractionService.CreateSpace(request);

            if (space.Left != null)
            {
                return Ok(space.Left);
            }
            else
            {
                return BadRequest(space.Right!);
            }
        }

        [HttpPut("{spaceId}")]
        [ProducesResponseType(typeof(SpaceResponse), 200)]
        [ProducesResponseType(typeof(ValidationError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Update(string spaceId, CreateOrUpdateSpaceRequest request)
        {
            if (spaceId != request.SpaceId)
            {
                return BadRequest(new ValidationError("spaceId", "INVALID_SPACE_ID"));
            }

            var space = this._spaceInteractionService.UpdateSpace(request);

            if (space.Left == null && space.Right == null)
            {
                return NotFound();
            }
            else if (space.Left != null)
            {
                return Ok(space.Left);
            }
            
            return BadRequest(space.Right);
        }

        [HttpDelete("{spaceId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSpace(string spaceId)
        {
            var deleteError = this._spaceInteractionService.DeleteSpaceBySpaceId(spaceId);

            if (deleteError != null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}