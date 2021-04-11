namespace Howler.Services.InteractionServices
{
    using System.Collections.Generic;
    using Howler.Services.Models;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;

    public interface ISpaceInteractionService
    {
        SpaceResponse? GetSpaceBySpaceId(string spaceId);
        
        Either<SpaceResponse, ValidationError> CreateSpace(CreateOrUpdateSpaceRequest request);
        
        Either<SpaceResponse?, ValidationError> UpdateSpace(CreateOrUpdateSpaceRequest request);
        
        ValidationError? DeleteSpaceBySpaceId(string spaceId);

        IEnumerable<ChannelInfoResponse>? GetChannelsForSpace(string spaceId);
    }
}