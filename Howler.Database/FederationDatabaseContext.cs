// <copyright file="FederationDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System.Linq;
    using Cassandra.Data.Linq;
    using Howler.Database.Models;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class FederationDatabaseContext : IFederationDatabaseContext
    {
        private IDatabaseClient _databaseClient;

        private ILogger<FederationDatabaseContext> _logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FederationDatabaseContext"/> class.
        /// </summary>
        /// <param name="databaseClient">
        /// An injected instance of the database client.
        /// </param>
        /// <param name="logger">An injected instance of the logger.</param>
        public FederationDatabaseContext(
            IDatabaseClient databaseClient,
            ILogger<FederationDatabaseContext> logger)
        {
            this._databaseClient = databaseClient;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public IQueryable<Space> Spaces =>
            new Table<Space>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<SpaceVanityUrl> SpaceVanityUrls =>
            new Table<SpaceVanityUrl>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<Server> Servers =>
            new Table<Server>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<UserSpaces> UserSpaces =>
            new Table<UserSpaces>(this._databaseClient.Session);
    }
}