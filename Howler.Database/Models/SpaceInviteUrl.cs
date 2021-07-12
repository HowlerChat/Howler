// <copyright file="SpaceInviteUrl.cs" company="Howler Team">
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
    /// The SpaceInviteUrl data model.
    /// </summary>
    [Table("space_invite_urls")]
    public class SpaceInviteUrl : IIncrementingCountEntity<string>
    {
        /// <summary>
        /// Gets or sets the invite URL for the space.
        /// </summary>
        [Column("invite_url")]
        public string? InviteUrl { get; set; }

        /// <summary>
        /// Gets or sets the space identifier.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the landing channel identifier.
        /// </summary>
        [Column("landing_channel_id")]
        public string? LandingChannelId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier creating the invite.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the expiration type.
        /// </summary>
        [Column("space_id")]
        public string? ExpireType { get; set; }

        /// <summary>
        /// Gets or sets the optional expiration date.
        /// </summary>
        [Column("expires_after")]
        public DateTime? ExpiresAfter { get; set; }

        /// <inheritdoc/>
        public string Key { get => this.InviteUrl!; }

        /// <inheritdoc/>
        public string CounterTable { get => "space_invite_url_counter"; }

        /// <inheritdoc/>
        public string CounterColumn { get => "invite_counter"; }
    }
}