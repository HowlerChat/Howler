// <copyright file="ConfigController.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Config.Controllers
{
    using System;
    using System.Linq;
    using Howler.Database;
    using Howler.Database.Config;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Config Controller.
    /// </summary>
    [Route("config")]
    [Authorize]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IConfigDatabaseContext _configDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigController"/>
        /// class.
        /// </summary>
        /// <param name="configDatabaseContext">
        /// An injected instance of the database.
        /// </param>
        public ConfigController(IConfigDatabaseContext configDatabaseContext)
        {
            this._configDatabaseContext = configDatabaseContext;
        }
    }
}
