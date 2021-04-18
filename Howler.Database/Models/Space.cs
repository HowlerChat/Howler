// <copyright file="Space.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using System;

    /// <summary>
    /// The Space data model.
    /// </summary>
    public class Space
    {
        /// <summary>
        /// Gets or sets the space identifier hash.
        /// </summary>
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the space name.
        /// </summary>
        public string? SpaceName { get; set; }

        /// <summary>
        /// Gets or sets the optional space description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the optional vanity URL for the space.
        /// </summary>
        public string? VanityUrl { get; set; }

        /// <summary>
        /// Gets or sets the hosting server URL.
        /// </summary>
        public string? ServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the space's creation date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the space's last modified date.
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}