namespace Howler.Services.Models.V1.Channel
{
    using System;

    /// <summary>
    /// A response class conveying information about a channel.
    /// </summary>
    public class ChannelInfoResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelInfoResponse"/>
        /// class.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="channelName">The channel name.</param>
        /// <param name="createdDate">The creation date of the channel.</param>
        /// <param name="modifiedDate">The last modified date of the channel.</param>
        /// <param name="channelDescription">The optional description for the channel.</param>
        public ChannelInfoResponse(
            string channelId,
            string channelName,
            DateTime createdDate,
            DateTime modifiedDate,
            string? channelDescription = null)
        {
            this.ChannelId = channelId;
            this.ChannelName = channelName;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
            this.ChannelDescription = channelDescription;
        }

        /// <summary>
        /// Gets the channel identifier.
        /// </summary> 
        public string ChannelId { get; }

        /// <summary>
        /// Gets the channel name.
        /// </summary>
        public string ChannelName { get; }

        /// <summary>
        /// Gets the channel description.
        /// </summary>
        public string? ChannelDescription { get; }

        /// <summary>
        /// Gets the channel's creation date.
        /// </summary>
        public DateTime CreatedDate { get; }
        
        /// <summary>
        /// Gets the channel's last modified date.
        /// </summary>
        public DateTime ModifiedDate { get; }
    }
}