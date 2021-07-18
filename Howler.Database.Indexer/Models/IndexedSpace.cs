// <copyright file="IndexedSpace.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Indexer.Models
{
    using System;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The IndexedSpace data model.
    /// </summary>
    [Table("indexed_spaces")]
    public class IndexedSpace : IIncrementingCountEntity<Tuple<string, string>>
    {
        /// <summary>
        /// Gets or sets the hosting server id.
        /// </summary>
        [Column("server_id")]
        public string? ServerId { get; set; }

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
        /// Gets or sets the icon URL.
        /// </summary>
        [Column("icon_url")]
        public string? IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the banner URL.
        /// </summary>
        [Column("banner_url")]
        public string? BannerUrl { get; set; }

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

        /// <inheritdoc/>
        public Tuple<string, string> Key
        {
            get => new Tuple<string, string>(this.ServerId!, this.SpaceId!);
        }

        /// <inheritdoc/>
        public string CounterTable { get => "indexed_space_statistics"; }

        /// <inheritdoc/>
        public string CounterColumn { get => "user_count"; }
    }
}