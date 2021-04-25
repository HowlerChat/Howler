// <copyright file="FederationDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System.Linq;
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
        public IQueryable<Scope> Scopes =>
            throw new System.NotSupportedException();

        /// <inheritdoc/>
        public IQueryable<Space> Spaces =>
            throw new System.NotSupportedException();
    }
}