// <copyright file="UserSpacesResponse.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Federation
{
    using System.Collections.Generic;

    /// <summary>
    /// A response of user's spaces.
    /// </summary>
    public class UserSpacesResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSpacesResponse"/>
        /// class.
        /// </summary>
        /// <param name="userSpaces">The source userspaces.</param>
        public UserSpacesResponse(Database.Config.Models.UserSpaces userSpaces)
        {
            this.UserId = userSpaces.UserId;
            this.SpaceIds = userSpaces.SpaceIds;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the spaces ids.
        /// </summary>
        public IEnumerable<string>? SpaceIds { get; set; }
    }
}