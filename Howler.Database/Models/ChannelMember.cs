// <copyright file="ChannelMember.cs" company="Howler Team">
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
    /// The ChannelMember data model.
    /// </summary>
    [Table("channel_members")]
    public class ChannelMember : IEntity<Tuple<string, string>>
    {
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

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the channel-specific profile data. This follows the
        /// same hierarchical precedence as the space member profiles do.
        /// </summary>
        [Column("profile_data")]
        public byte[]? ProfileData { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public Tuple<string, string> Key
        {
            get => new Tuple<string, string>(this.ChannelId!, this.MemberId!);
        }
    }
}