// <copyright file="SpaceVanityUrl.cs" company="Howler Team">
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
    /// The SpaceVanityUrl data model.
    /// </summary>
    [Table("howler_indexer.space_vanity_urls")]
    public class SpaceVanityUrl : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the vanity URL for the space.
        /// </summary>
        [Column("vanity_url")]
        public string? VanityUrl { get; set; }

        /// <summary>
        /// Gets or sets the space identifier.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <inheritdoc/>
        public string Key { get => this.VanityUrl!; }
    }
}