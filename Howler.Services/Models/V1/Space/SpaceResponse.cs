// <copyright file="SpaceResponse.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Space
{
    using System;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// A response class containing space information.
    /// </summary>
    public class SpaceResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceResponse"/>
        /// class.
        /// </summary>
        /// <param name="space">The source space.</param>
        public SpaceResponse(Database.Core.Models.Space space)
        {
            this.SpaceId = space.SpaceId;
            this.SpaceName = space.SpaceName;
            this.Description = space.Description;
            this.VanityUrl = space.VanityUrl;
            this.ServerUrl = space.ServerUrl;
            this.CreatedDate = space.CreatedDate;
            this.ModifiedDate = space.ModifiedDate;
            this.UserId = space.UserId;
            this.DefaultChannelId = space.DefaultChannelId;
            this.ChannelGroups = space.ChannelGroups != null ?
                JsonConvert.DeserializeObject<ChannelGroupResponse>(
                    Encoding.UTF8.GetString(space.ChannelGroups)) :
                new ChannelGroupResponse();
        }

        /// <summary>
        /// Gets the space identifier hash.
        /// </summary>
        public string? SpaceId { get; }

        /// <summary>
        /// Gets the space name.
        /// </summary>
        public string? SpaceName { get; }

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
        public string? ServerUrl { get; }

        /// <summary>
        /// Gets the server owner id.
        /// </summary>
        public string? UserId { get; }

        /// <summary>
        /// Gets the default channel id.
        /// </summary>
        public string? DefaultChannelId { get; }

        /// <summary>
        /// Gets the space's creation date.
        /// </summary>
        public DateTime CreatedDate { get; }

        /// <summary>
        /// Gets the space's last modified date.
        /// </summary>
        public DateTime ModifiedDate { get; }

        /// <summary>
        /// Gets the space's channel groups.
        /// </summary>
        public ChannelGroupResponse ChannelGroups { get; }
    }
}