// <copyright file="CoreDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Core
{
    using System;
    using System.Linq;
    using Cassandra.Data.Linq;
    using Howler.Database.Core.Models;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class CoreDatabaseContext : ICoreDatabaseContext
    {
        private IDatabaseClient _databaseClient;

        private ILogger<CoreDatabaseContext> _logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="CoreDatabaseContext"/> class.
        /// </summary>
        /// <param name="databaseClient">
        /// An injected instance of the database client.
        /// </param>
        /// <param name="logger">An injected instance of the logger.</param>
        public CoreDatabaseContext(
            IDatabaseClient databaseClient,
            ILogger<CoreDatabaseContext> logger)
        {
            this._databaseClient = databaseClient;
            this._databaseClient.Session.ChangeKeyspace(
                Environment.GetEnvironmentVariable("HOWLER_KEYSPACE_CORE") ??
                throw new ArgumentNullException(
                    "HOWLER_KEYSPACE_CONFIG is null. Please define it in" +
                    " your environment variables."));
            this._logger = logger;
        }

        /// <inheritdoc/>
        public IQueryable<Space> Spaces =>
            new Table<Space>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<Channel> Channels =>
            new Table<Channel>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<ChannelMember> ChannelMembers =>
            new Table<ChannelMember>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<ChannelMemberState> ChannelMemberStates =>
            new Table<ChannelMemberState>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<Message> Messages =>
            new Table<Message>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<MessageIndex> MessageIndex =>
            new Table<MessageIndex>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<SpaceBan> SpaceBans =>
            new Table<SpaceBan>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<SpaceHistory> SpaceHistory =>
            new Table<SpaceHistory>(this._databaseClient.Session);

        /// <inheritdoc/>
        public IQueryable<SpaceMember> SpaceMembers =>
            new Table<SpaceMember>(this._databaseClient.Session);
    }
}