namespace Howler.Services.Models.V1.Space
{
    using System;

    public class SpaceResponse
    {
        public SpaceResponse(string spaceId, string spaceName, string serverUrl) =>
            (this.SpaceId, this.SpaceName, this.ServerUrl) = (spaceId, spaceName, serverUrl);

        /// <summary>
        /// Represents the Space, as a hash
        /// </summary>
        public string SpaceId { get; set; }

        public string SpaceName { get; set; }

        public string? Description { get; set; }

        public string? VanityUrl { get; set; }

        public string ServerUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}