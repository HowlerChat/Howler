// <copyright file="UserAuthorizers.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Config.Models
{
    using System.Collections.Generic;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The UserAuthorizers data model.
    /// </summary>
    [Table("user_authorizers")]
    public class UserAuthorizers : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the authorizers.
        /// </summary>
        [Column("authorizers")]
        public Dictionary<string, string>? Authorizers { get; set; }

        /// <inheritdoc/>
        public string Key { get => this.UserId!; }
    }
}