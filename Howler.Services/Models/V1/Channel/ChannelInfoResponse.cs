namespace Howler.Services.Models.V1.Channel
{
    using System;

    public class ChannelInfoResponse
    {
        public ChannelInfoResponse(string channelId, string channelName, DateTime createdDate, DateTime modifiedDate, string? channelDescription = null) =>
            (this.ChannelId, this.ChannelName, this.CreatedDate, this.ModifiedDate, this.ChannelDescription) = (channelId, channelName, createdDate, modifiedDate, channelDescription);

        public string ChannelId { get; set; }

        public string ChannelName { get; set; }

        public string? ChannelDescription { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }
    }
}