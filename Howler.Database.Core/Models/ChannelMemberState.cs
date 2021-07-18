// <copyright file="ChannelMemberState.cs" company="Howler Team">
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
    /// The ChannelMemberState data model.
    /// </summary>
    [Table("channel_member_states")]
    public class ChannelMemberState : IEntity<Tuple<string, string, string>>
    {
        /// <summary>
        /// Gets or sets the space identifier. Set to "direct" for direct
        /// or group messaging.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        [Column("channel_id")]
        public string? ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the channel member id.
        /// </summary>
        [Column("member_id")]
        public string? MemberId { get; set; }

        /// <summary>
        /// Gets or sets the last read message identifier.
        /// </summary>
        [Column("last_read_message_id")]
        public string? LastReadMessageId { get; set; }

        /// <summary>
        /// Gets or sets the optional mention count.
        /// </summary>
        [Column("mention_count")]
        public int? MentionCount { get; set; }

        /// <summary>
        /// Gets or sets the optional mention type.
        /// </summary>
        [Column("mention_type")]
        public string? MentionType { get; set; }

        /// <inheritdoc/>
        public Tuple<string, string, string> Key
        {
            get => new Tuple<string, string, string>(
                this.SpaceId!,
                this.MemberId!,
                this.ChannelId!);
        }
    }
}