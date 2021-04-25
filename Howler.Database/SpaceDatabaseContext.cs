// <copyright file="SpaceDatabaseContext.cs" company="Howler Team">
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
    public class SpaceDatabaseContext : ISpaceDatabaseContext
    {
        private IDatabaseClient _databaseClient;

        private ILogger<SpaceDatabaseContext> _logger;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SpaceDatabaseContext"/> class.
        /// </summary>
        /// <param name="databaseClient">
        /// An injected instance of the database client.
        /// </param>
        /// <param name="logger">An injected instance of the logger.</param>
        public SpaceDatabaseContext(
            IDatabaseClient databaseClient,
            ILogger<SpaceDatabaseContext> logger)
        {
            this._databaseClient = databaseClient;
            this._logger = logger;
        }

        /// <inheritdoc/>
        public IQueryable<Space> Spaces =>
            throw new System.NotSupportedException();

        /// <inheritdoc/>
        public IQueryable<Channel> Channels =>
            throw new System.NotSupportedException();
    }
}