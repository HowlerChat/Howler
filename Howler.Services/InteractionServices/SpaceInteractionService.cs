namespace Howler.Services.InteractionServices
{
    using System.Collections.Generic;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.Extensions.Logging;

    public class SpaceInteractionService : ISpaceInteractionService
    {
        private ILogger<SpaceInteractionService> _logger;
        
        public SpaceInteractionService(ILogger<SpaceInteractionService> logger)
        {
            this._logger = logger;
        }

        public Models.Either<SpaceResponse, ValidationError> CreateSpace(CreateOrUpdateSpaceRequest request)
        {
            if (request.SpaceId == "asdf")
            {
                return new Models.Either<SpaceResponse, ValidationError>(new SpaceResponse(request.SpaceId, "Test", "https://us-west-2.howler.chat/"));
            }
            else
            {
                return new Models.Either<SpaceResponse, ValidationError>(new ValidationError("spaceId", "INVALID_SPACE_ID"));
            }
        }

        public ValidationError? DeleteSpaceBySpaceId(string spaceId)
        {
            if (spaceId == "asdf")
            {
                return null;
            }
            else
            {
                return new ValidationError("spaceId", "INVALID_SPACE_ID");
            }
        }

        public SpaceResponse? GetSpaceBySpaceId(string spaceId)
        {
            if (spaceId == "asdf")
            {
                return new SpaceResponse(spaceId, "Test", "https://us-west-2.howler.chat/");
            }
            else
            {
                return null;
            }
        }

        public Models.Either<SpaceResponse?, ValidationError> UpdateSpace(CreateOrUpdateSpaceRequest request)
        {
            if (request.SpaceId == "asdf")
            {
                return new Models.Either<SpaceResponse?, ValidationError>(new SpaceResponse(request.SpaceId, "Test", "https://us-west-2.howler.chat/"));
            }
            else
            {
                return new Models.Either<SpaceResponse?, ValidationError>((SpaceResponse?)null);
            }
        }

        public IEnumerable<ChannelInfoResponse>? GetChannelsForSpace(string spaceId)
        {
            return new List<ChannelInfoResponse>();
        }
    }
}