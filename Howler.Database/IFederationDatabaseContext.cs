// <copyright file="IFederationDatabaseContext.cs" company="Howler Team">
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
    /// A simple context for accessing the data store corresponding to
    /// federation.
    /// </summary>
    public interface IFederationDatabaseContext
    {
        /// <summary>
        /// Gets the Scopes.
        /// </summary>
        IQueryable<Scope> Scopes { get; }

        /// <summary>
        /// Gets the Spaces.
        /// </summary>
        IQueryable<Space> Spaces { get; }

        /// <summary>
        /// Gets the Servers.
        /// </summary>
        IQueryable<Server> Servers { get; }
    }
}