namespace Howler.Services.Models.V1.Space
{
    using System;

    /// <summary>
    /// A response class containing space information.
    /// </summary>
    public class SpaceResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceResponse"/>
        /// class.
        /// </summary>
        /// <param name="spaceId">The space identifier.</param>
        /// <param name="spaceName">The space name.</param>
        /// <param name="serverUrl">
        /// The URL of the server hosting the space.
        /// </param>
        /// <param name="createdDate">
        /// The creation date of the space.
        /// </param>
        /// <param name="modifiedDate">
        /// The last modified date of the space.
        /// </param>
        public SpaceResponse(
            string spaceId,
            string spaceName,
            string serverUrl,
            DateTime createdDate,
            DateTime modifiedDate)
        {
            this.SpaceId = spaceId;
            this.SpaceName = spaceName;
            this.ServerUrl = serverUrl;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
        }

        /// <summary>
        /// Gets the space identifier hash.
        /// </summary>
        public string SpaceId { get; }

        /// <summary>
        /// Gets the space name.
        /// </summary>
        public string SpaceName { get; }

        /// <summary>
        /// Gets the optional space description.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Gets the optional vanity URL for the space.
        /// </summary>
        public string? VanityUrl { get; }

        /// <summary>
        /// Gets the hosting server URL.
        /// </summary>
        public string ServerUrl { get; }

        /// <summary>
        /// Gets the space's creation date.
        /// </summary>
        public DateTime CreatedDate { get; }

        /// <summary>
        /// Gets the space's last modified date.
        /// </summary>
        public DateTime ModifiedDate { get; }
    }
}