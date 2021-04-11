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
    [Route("v1/spaces/{spaceId}/channels")]
    //[Authorize]
    public class ChannelController : ControllerBase
    {
        private readonly ILogger<ChannelController> _logger;

        private readonly ISpaceInteractionService _spaceInteractionService;

        public ChannelController(ILogger<ChannelController> logger, ISpaceInteractionService spaceInteractionService)
        {
            _logger = logger;
            _spaceInteractionService = spaceInteractionService;
        }

        [HttpGet]
        public ActionResult GetChannelList(string spaceId)
        {
            var channels = this._spaceInteractionService.GetChannelsForSpace(spaceId);

            return Ok();
        }
    }
}