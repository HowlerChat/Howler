// <copyright file="IndexController.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Indexer.Controllers
{
    using System;
    using System.Linq;
    using Howler.Database;
    using Howler.Database.Indexer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Indexed Space Controller.
    /// </summary>
    [Route("spaces")]
    [Authorize]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private IIndexerDatabaseContext _indexerDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexController"/>
        /// class.
        /// </summary>
        /// <param name="indexerDatabaseContext">
        /// An injected instance of the database.
        /// </param>
        public IndexController(IIndexerDatabaseContext indexerDatabaseContext)
        {
            this._indexerDatabaseContext = indexerDatabaseContext;
        }
    }
}
