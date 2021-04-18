// <copyright file="CassandraClient.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System;
    using Cassandra;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// A client for accessing an active Cassandra session object.
    /// </summary>
    public class CassandraClient : IDatabaseClient
    {
        private ILogger<CassandraClient> _logger;

        private ICluster _cluster;

        /// <summary>
        /// Initializes a new instance of the <see cref="CassandraClient"/>
        /// class.
        /// </summary>
        /// <param name="logger">An injected instance of the logger.</param>
        public CassandraClient(ILogger<CassandraClient> logger)
        {
            this._logger = logger;
            this._cluster = Cluster.Builder()
                .AddContactPoint(Environment.GetEnvironmentVariable(
                    "HOWLER_CASSANDRA_ENDPOINT") ??
                    throw new ArgumentNullException(
                    "HOWLER_CASSANDRA_ENDPOINT is null. Please define it in" +
                    "your environment variable."))
                .Build();
            this.Session = this._cluster.Connect(
                Environment.GetEnvironmentVariable("HOWLER_KEYSPACE") ??
                throw new ArgumentNullException(
                    "HOWLER_KEYSPACE is null. Please define it in your" +
                    " environment variables."));
        }

        /// <inheritdoc/>
        public ISession Session { get; }
    }
}