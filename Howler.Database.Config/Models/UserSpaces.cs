// <copyright file="UserSpaces.cs" company="Howler Team">
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
    /// The UserSpaces data model.
    /// </summary>
    [Table("howler_config.user_spaces")]
    public class UserSpaces : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the spaces.
        /// </summary>
        [Column("space_ids")]
        public IEnumerable<string>? SpaceIds { get; set; }

        /// <inheritdoc/>
        public string Key { get => this.UserId!; }
    }
}