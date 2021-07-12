// <copyright file="SpaceBan.cs" company="Howler Team">
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
    /// The SpaceBan data model.
    /// </summary>
    [Table("space_bans")]
    public class SpaceBan : IEntity<Tuple<string, string, DateTime>>
    {
        /// <summary>
        /// Gets or sets the space identifier.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the member identifier.
        /// </summary>
        [Column("member_id")]
        public string? MemberId { get; set; }

        /// <summary>
        /// Gets or sets the ban's effective date.
        /// </summary>
        [Column("effective_date")]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Gets or sets the ban's optional end date.
        /// </summary>
        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the reason text for the ban.
        /// </summary>
        [Column("reason_text")]
        public string? ReasonText { get; set; }

        /// <summary>
        /// Gets or sets the optional message id related to the user's ban.
        /// </summary>
        [Column("reason_message_id")]
        public string? ReasonMessageId { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the optional copy of the message content that resulted
        /// in the ban. This must be provided through the original message
        /// reference to retain chain of custody in the event of unsigned
        /// messages.
        /// </summary>
        [Column("space_policy")]
        public byte[]? ReasonMessageContent { get; set; }
#pragma warning restore SA1011

        /// <summary>
        /// Gets or sets the id of the member performing the ban.
        /// </summary>
        [Column("banning_member_id")]
        public string? BanningMemberId { get; set; }

        /// <inheritdoc/>
        public Tuple<string, string, DateTime> Key
        {
            get => new Tuple<string, string, DateTime>(
                this.SpaceId!,
                this.MemberId!,
                this.EffectiveDate);
        }
    }
}