// <copyright file="IIndexerDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Indexer
{
    using System.Linq;
    using Howler.Database.Indexer.Models;

    /// <summary>
    /// A simple context for accessing the data store corresponding to
    /// federation.
    /// </summary>
    public interface IIndexerDatabaseContext
    {
        /// <summary>
        /// Gets the IndexedSpaces.
        /// </summary>
        IQueryable<IndexedSpace> IndexedSpaces { get; }

        /// <summary>
        /// Gets the Servers.
        /// </summary>
        IQueryable<Server> Servers { get; }

        /// <summary>
        /// Gets the SpaceVanityUrls.
        /// </summary>
        IQueryable<SpaceVanityUrl> SpaceVanityUrls { get; }

        /// <summary>
        /// Gets the Blacklists.
        /// </summary>
        IQueryable<Blacklist> Blacklists { get; }

        /// <summary>
        /// Gets the SpaceInviteUrls.
        /// </summary>
        IQueryable<SpaceInviteUrl> SpaceInviteUrls { get; }

        /// <summary>
        /// Gets the UserProfiles.
        /// </summary>
        IQueryable<UserProfile> UserProfiles { get; }

        /// <summary>
        /// Gets the Whitelists.
        /// </summary>
        IQueryable<Whitelist> Whitelists { get; }
    }
}