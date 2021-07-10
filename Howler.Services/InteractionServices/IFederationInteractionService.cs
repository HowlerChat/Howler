// <copyright file="IFederationInteractionService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.InteractionServices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Howler.Services.Models;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Federation;
    using Howler.Services.Models.V1.Space;

    /// <summary>
    /// Defines the interaction boundaries with server federation and
    /// coordinates control.
    /// </summary>
    public interface IFederationInteractionService
    {
        /// <summary>
        /// Retrieves a collection of user spaces.
        /// </summary>
        /// <returns>
        /// Returns a <see cref="UserSpacesResponse"/>.
        /// </returns>
        Task<UserSpacesResponse?> GetUserSpaces();

        /// <summary>
        /// Joins a space.
        /// </summary>
        /// <param name="spaceId">The space to join.</param>
        /// <returns>
        /// Returns on completion.
        /// </returns>
        Task JoinSpace(string spaceId);

        /// <summary>
        /// Retrieves a space by its url.
        /// </summary>
        /// <param name="url">The url of the space.</param>
        /// <returns>
        /// Returns a <see cref="SpaceResponse"/>.
        /// </returns>
        Task<SpaceResponse?> FindSpaceByUrl(string url);
    }
}