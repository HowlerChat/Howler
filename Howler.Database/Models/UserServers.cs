// <copyright file="UserServers.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using System.Collections.Generic;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The UserServers data model.
    /// </summary>
    [Table("user_servers")]
    public class UserServers : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the servers.
        /// </summary>
        [Column("server_ids")]
        public IEnumerable<string>? ServerIds { get; set; }

        /// <inheritdoc/>
        public string Key { get => this.UserId!; }
    }
}