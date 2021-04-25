// <copyright file="Channel.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using System;

    /// <summary>
    /// The Channel data model.
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Gets or sets the channel identifier hash.
        /// </summary>
        public string? ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the space identifier hash.
        /// </summary>
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the channel name.
        /// </summary>
        public string? ChannelName { get; set; }

        /// <summary>
        /// Gets or sets the optional channel description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the policy document.
        /// </summary>
        public Policy? Policy { get; set; }

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