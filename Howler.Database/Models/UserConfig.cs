// <copyright file="UserConfig.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The UserConfig data model.
    /// </summary>
    [Table("user_config")]
    public class UserConfig : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        [Column("config")]
        public byte[]? Config { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public string Key { get => this.UserId!; }
    }
}