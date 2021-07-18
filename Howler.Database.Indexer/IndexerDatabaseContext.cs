// <copyright file="IndexerDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Indexer
{
    using System;
    using System.Linq;
    using Cassandra.Data.Linq;
    using Howler.Database.Indexer.Models;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class IndexerDatabaseContext : IIndexerDatabaseContext
    {
        private IDatabaseClient _databaseClient;

        private ILogger<IndexerDatabaseContext> _logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="IndexerDatabaseContext"/> class.
        /// </summary>
        /// <param name="databaseClient">
        /// An injected instance of the database client.
        /// </param>
        /// <param name="logger">An injected instance of the logger.</param>
        public IndexerDatabaseContext(
            IDatabaseClient databaseClient,
            ILogger<IndexerDatabaseContext> logger)
        {
            this._databaseClient = databaseClient;
            this._databaseClient.Session.ChangeKeyspace(
                Environment.GetEnvironmentVariable(
                    "HOWLER_KEYSPACE_INDEXER") ??
                throw new ArgumentNullException(
                    "HOWLER_KEYSPACE_INDEXER is null. Please define it in" +
                    " your environment variables."));
            this._logger = logger;
        }

        /// <inheritdoc/>
        public IQueryable<IndexedSpace> IndexedSpaces =>
            new Table<IndexedSpace>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<SpaceVanityUrl> SpaceVanityUrls =>
            new Table<SpaceVanityUrl>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<Server> Servers =>
            new Table<Server>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<Blacklist> Blacklists =>
            new Table<Blacklist>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<SpaceInviteUrl> SpaceInviteUrls =>
            new Table<SpaceInviteUrl>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<UserProfile> UserProfiles =>
            new Table<UserProfile>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<Whitelist> Whitelists =>
            new Table<Whitelist>(this._databaseClient.Session);
    }
}