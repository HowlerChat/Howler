namespace Howler.Services.InteractionServices
{
    using System;
    using System.Collections.Generic;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// An interaction service for managing spaces.
    /// </summary>
    public class SpaceInteractionService : ISpaceInteractionService
    {
        private ILogger<SpaceInteractionService> _logger;
        
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SpaceInteractionService"/> class.
        /// </summary>
        /// <param name="logger">An injected logger instance.</param>
        public SpaceInteractionService(ILogger<SpaceInteractionService> logger)
        {
            this._logger = logger;
        }

        /// <inheritdoc/>
        public Models.Either<SpaceResponse, ValidationError> CreateSpace(CreateOrUpdateSpaceRequest request)
        {
            if (request.SpaceId == "asdf")
            {
                return new Models.Either<SpaceResponse, ValidationError>(
                    new SpaceResponse(
                        request.SpaceId,
                        "Test",
                        "https://us-west-2.howler.chat/",
                        DateTime.UtcNow,
                        DateTime.UtcNow));
            }
            else
            {
                return new Models.Either<SpaceResponse, ValidationError>(new ValidationError("spaceId", "INVALID_SPACE_ID"));
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public SpaceResponse? GetSpaceBySpaceId(string spaceId)
        {
            if (spaceId == "asdf")
            {
                return new SpaceResponse(
                        spaceId,
                        "Test",
                        "https://us-west-2.howler.chat/",
                        DateTime.UtcNow,
                        DateTime.UtcNow);
            }
            else
            {
                return null;
            }
        }

        /// <inheritdoc/>
        public Models.Either<SpaceResponse?, ValidationError> UpdateSpace(CreateOrUpdateSpaceRequest request)
        {
            if (request.SpaceId == "asdf")
            {
                return new Models.Either<SpaceResponse?, ValidationError>(
                    new SpaceResponse(
                        request.SpaceId,
                        "Test",
                        "https://us-west-2.howler.chat/",
                        DateTime.UtcNow,
                        DateTime.UtcNow));
            }
            else
            {
                return new Models.Either<SpaceResponse?, ValidationError>((SpaceResponse?)null);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ChannelInfoResponse>? GetChannelsForSpace(string spaceId)
        {
            return new List<ChannelInfoResponse>();
        }
    }
}