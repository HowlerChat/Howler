// <copyright file="ConfigDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Config
{
    using System;
    using System.Linq;
    using Cassandra.Data.Linq;
    using Howler.Database.Config.Models;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class ConfigDatabaseContext : IConfigDatabaseContext
    {
        private IDatabaseClient _databaseClient;

        private ILogger<ConfigDatabaseContext> _logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ConfigDatabaseContext"/> class.
        /// </summary>
        /// <param name="databaseClient">
        /// An injected instance of the database client.
        /// </param>
        /// <param name="logger">An injected instance of the logger.</param>
        public ConfigDatabaseContext(
            IDatabaseClient databaseClient,
            ILogger<ConfigDatabaseContext> logger)
        {
            this._databaseClient = databaseClient;
            this._databaseClient.Session.ChangeKeyspace(
                Environment.GetEnvironmentVariable("HOWLER_KEYSPACE_CONFIG") ??
                throw new ArgumentNullException(
                    "HOWLER_KEYSPACE_CONFIG is null. Please define it in" +
                    " your environment variables."));
            this._logger = logger;
        }

        /// <inheritdoc/>
        public IQueryable<UserAuthorizers> UserAuthorizers
        {
            get => new Table<UserAuthorizers>(this._databaseClient.Session);
        }

        /// <inheritdoc/>
        public IQueryable<UserConfig> UserConfig
        {
            get => new Table<UserConfig>(this._databaseClient.Session);
        }

        /// <inheritdoc/>
        public IQueryable<UserServers> UserServers
        {
            get => new Table<UserServers>(this._databaseClient.Session);
        }

        /// <inheritdoc/>
        public IQueryable<UserSpaces> UserSpaces
        {
            get => new Table<UserSpaces>(this._databaseClient.Session);
        }
    }
}