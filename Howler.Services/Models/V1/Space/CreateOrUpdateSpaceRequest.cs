// <copyright file="CreateOrUpdateSpaceRequest.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Space
{
    /// <summary>
    /// A request class to create or update a space.
    /// </summary>
    public class CreateOrUpdateSpaceRequest
    {
        /// <summary>
        /// Gets or sets the space identifier.
        /// </summary>
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the space.
        /// </summary>
        public string? SpaceName { get; set; }

        /// <summary>
        /// Gets or sets the space description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the vanity URL for the space.
        /// </summary>
        public string? VanityUrl { get; set; }

        /// <summary>
        /// Gets or sets the server URL.
        /// </summary>
        public string? ServerUrl { get; set; }

        /// <summary>
        /// Gets or sets the default channel id.
        /// </summary>
        public string? DefaultChannelId { get; set; }
    }
}