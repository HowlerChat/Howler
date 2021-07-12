// <copyright file="ISpaceDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System.Linq;
    using Howler.Database.Models;

    /// <summary>
    /// A simple context for accessing the data store corresponding to spaces.
    /// </summary>
    public interface ISpaceDatabaseContext
    {
        /// <summary>
        /// Gets the Spaces.
        /// </summary>
        IQueryable<Space> Spaces { get; }

        /// <summary>
        /// Gets the SpaceVanityUrls.
        /// </summary>
        IQueryable<SpaceVanityUrl> SpaceVanityUrls { get; }

        /// <summary>
        /// Gets the Channels.
        /// </summary>
        IQueryable<Channel> Channels { get; }
    }
}