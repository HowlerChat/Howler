// <copyright file="MessageIndex.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Core.Models
{
    using System;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The MessageIndex data model.
    /// </summary>
    [Table("howler.messages_index")]
    public class MessageIndex : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        [Column("message_id")]
        public string? MessageId { get; set; }

        /// <summary>
        /// Gets or sets the space id. Set to "direct" for direct/group
        /// messaging.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        [Column("channel_id")]
        public string? ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the epoch. Epoch is calculated from the creation
        /// timestamp using a configured bucketing window (presently 10
        /// minutes).
        ///
        /// Calculation:
        /// (DateTimeOffset)this.CreatedDate.ToUnixTimeSeconds() / 600
        ///
        /// Theoretically assuming max 4K message payloads this will result in
        /// about 2,560 messages within ten minutes hitting the partition
        /// performance threshold (10MB), so if we hit space hotspotting we
        /// can just move the bucket window to a space configurable value,
        /// preferably an auto-adjusting one to prevent jerks.
        /// </summary>
        [Column("epoch")]
        public string? Epoch { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        public string Key
        {
            get => this.MessageId!;
        }
    }
}