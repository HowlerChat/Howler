namespace Howler.Services.Models.V1.Space
{
    public class CreateOrUpdateSpaceRequest
    {
        /// <summary>
        /// Represents the Space, as a hash
        /// </summary>
        public string? SpaceId { get; set; }

        public string? SpaceName { get; set; }

        public string? Description { get; set; }

        public string? VanityUrl { get; set; }

        public string? ServerUrl { get; set; }
    }
}