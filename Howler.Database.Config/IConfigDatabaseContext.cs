// <copyright file="IConfigDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Config
{
    using System.Linq;
    using Howler.Database.Config.Models;

    /// <summary>
    /// A simple context for accessing the data store corresponding to config.
    /// </summary>
    public interface IConfigDatabaseContext
    {
        /// <summary>
        /// Gets the UserAuthorizers.
        /// </summary>
        IQueryable<UserAuthorizers> UserAuthorizers { get; }

        /// <summary>
        /// Gets the UserConfig.
        /// </summary>
        IQueryable<UserConfig> UserConfig { get; }

        /// <summary>
        /// Gets the UserServers.
        /// </summary>
        IQueryable<UserServers> UserServers { get; }

        /// <summary>
        /// Gets the UserSpaces.
        /// </summary>
        IQueryable<UserSpaces> UserSpaces { get; }
    }
}