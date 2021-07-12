// <copyright file="Space.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using System;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The Space data model.
    /// </summary>
    [Table("spaces")]
    public class Space : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the space identifier.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the space name.
        /// </summary>
        [Column("space_name")]
        public string? SpaceName { get; set; }

        /// <summary>
        /// Gets or sets the optional space description.
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the optional vanity URL for the space.
        /// </summary>
        [Column("vanity_url")]
        public string? VanityUrl { get; set; }

        /// <summary>
        /// Gets or sets the hosting server URL.
        /// </summary>
        [Column("server_url")]
        public string? ServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the icon URL.
        /// </summary>
        [Column("icon_url")]
        public string? IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the banner URL.
        /// </summary>
        [Column("banner_url")]
        public string? BannerUrl { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the policy document.
        /// </summary>
        [Column("space_policy")]
        public byte[]? SpacePolicy { get; set; }
#pragma warning restore SA1011

        /// <summary>
        /// Gets or sets the space's creation date.
        /// </summary>
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the space's last modified date.
        /// </summary>
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the space's owner.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the space's default channel id.
        /// </summary>
        [Column("default_channel_id")]
        public string? DefaultChannelId { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the space's channel group list.
        /// </summary>
        [Column("channel_groups")]
        public byte[]? ChannelGroups { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public string Key { get => this.SpaceId!; }
    }
}