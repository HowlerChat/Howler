// <copyright file="Whitelist.cs" company="Howler Team">
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
    /// The Whitelist data model.
    /// </summary>
    [Table("howler_indexer.whitelist")]
    public class Whitelist : IEntity<Tuple<string, string>>
    {
        /// <summary>
        /// Gets or sets the entity identifier, as understood to the indexer.
        /// </summary>
        [Column("howler_id")]
        public string? HowlerId { get; set; }

        /// <summary>
        /// Gets or sets the entity type. Can be either "server" or "user".
        /// </summary>
        [Column("entity_type")]
        public string? EntityType { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [Column("reason")]
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets the whitelisting user id.
        /// </summary>
        [Column("whitelisting_user_id")]
        public string? BlacklistingUserId { get; set; }

        /// <summary>
        /// Gets or sets the whitelisting date.
        /// </summary>
        [Column("whitelisted_date")]
        public DateTime WhitelistedDate { get; set; }

        /// <summary>
        /// Gets or sets the optional whitelisting end date.
        /// </summary>
        [Column("whitelisted_until_date")]
        public DateTime? WhitelistedUntilDate { get; set; }

        /// <inheritdoc/>
        public Tuple<string, string> Key
        {
            get => new Tuple<string, string>(this.HowlerId!, this.EntityType!);
        }
    }
}