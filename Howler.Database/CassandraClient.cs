// <copyright file="CassandraClient.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using Cassandra;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

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
            var port = Environment
                .GetEnvironmentVariable("HOWLER_CASSANDRA_PORT");
            var isProd = Environment
                .GetEnvironmentVariable("HOWLER_ENVIRONMENT") == "prod";

            var builder = Cluster.Builder()
                .AddContactPoint(Environment.GetEnvironmentVariable(
                    "HOWLER_CASSANDRA_ENDPOINT") ??
                    throw new ArgumentNullException(
                    "HOWLER_CASSANDRA_ENDPOINT is null. Please define it in" +
                    "your environment variable."))
                .WithPort(port is null ? 9042 : int.Parse(port));

            if (isProd)
            {
                var username = Environment
                    .GetEnvironmentVariable("HOWLER_CASSANDRA_USERNAME");
                var password = Environment
                    .GetEnvironmentVariable("HOWLER_CASSANDRA_PASSWORD");
                var certs = new X509Certificate2Collection();
                var amazonRoot = new X509Certificate2(@"./AmazonRootCA1.pem");
                certs.Add(amazonRoot);

                builder.WithAuthProvider(
                        new PlainTextAuthProvider(username, password))
                    .WithSSL(new SSLOptions()
                    .SetCertificateCollection(certs));
            }

            this._cluster = builder.Build();

            try
            {
                this.Session = this._cluster.Connect(
                    Environment.GetEnvironmentVariable("HOWLER_KEYSPACE") ??
                    throw new ArgumentNullException(
                        "HOWLER_KEYSPACE is null. Please define it in your" +
                        " environment variables."));
            }
            catch (NoHostAvailableException e)
            {
                throw new Exception(e.ToString() +
                    JsonConvert.SerializeObject(
                        e.Errors.ToDictionary(
                            k => k.Key.ToString(),
                            v => v.Value.ToString())));
            }
        }

        /// <inheritdoc/>
        public ISession Session { get; }
    }
}