// <copyright file="HealthController.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Howler.Database;
    using Howler.Services.InteractionServices;
    using Howler.Services.Models.V1.Channel;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides actions to verify health of the Howler service API.
    /// </summary>
    [ApiController]
    [Route("v1/health")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        private readonly IDatabaseClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthController"/>
        /// class.
        /// </summary>
        /// <param name="logger">An injected instance of the logger.</param>
        /// <param name="client">
        /// An injected instance of the database client.
        /// </param>
        public HealthController(
            ILogger<HealthController> logger,
            IDatabaseClient client)
        {
            this._logger = logger;
            this._client = client;
        }

        /// <summary>
        /// Returns health status for the API.
        /// </summary>
        /// <returns>200 OK if healthy, 500 otherwise.</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Status()
        {
            try
            {
                if (this._client.Session != null)
                {
                    this._client.Session.Execute("SELECT * FROM spaces;");
                    return this.Ok();
                }
                else
                {
                    return this.StatusCode(500, "Session was null");
                }
            }
            catch (Exception e)
            {
                var env = Environment
                    .GetEnvironmentVariable("HOWLER_ENVIRONMENT");

                this._logger.LogCritical(
                    e,
                    "An exception was encountered while fetching health.");
                this._logger.LogCritical("HOWLER_KEYSPACE: " +
                    Environment.GetEnvironmentVariable("HOWLER_KEYSPACE"));

                if (env == "dev")
                {
                    return this.StatusCode(500, e.ToString());
                }
                else
                {
                    return this.StatusCode(500, "Session was null");
                }
            }
        }
    }
}