// <copyright file="Server.cs" company="Howler Team">
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
    /// The Server data model.
    /// </summary>
    [Table("howler_indexer.servers")]
    public class Server : IIncrementingCountEntity<string>
    {
        /// <summary>
        /// Gets or sets the server identifier.
        /// </summary>
        [Column("server_id")]
        public string? ServerId { get; set; }

        /// <summary>
        /// Gets or sets the space name.
        /// </summary>
        [Column("server_name")]
        public string? ServerName { get; set; }

        /// <summary>
        /// Gets or sets the optional space description.
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the hosting server URL.
        /// </summary>
        [Column("server_url")]
        public string? ServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the server's entitlements.
        /// </summary>
        [Column("entitlements")]
        public string? Entitlements { get; set; }

        /// <summary>
        /// Gets or sets the server's owner user id.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the server's ISO country code.
        /// </summary>
        [Column("country_id")]
        public string? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the server's creation date.
        /// </summary>
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the server's last modified date.
        /// </summary>
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the server's last modified date.
        /// </summary>
        [Column("blacklisted_date")]
        public DateTime BlacklistedDate { get; set; }

        /// <inheritdoc/>
        public string Key { get => this.ServerId!; }

        /// <inheritdoc/>
        public string CounterTable { get => "server_statistics"; }

        /// <inheritdoc/>
        public string CounterColumn { get => "user_count"; }
    }
}