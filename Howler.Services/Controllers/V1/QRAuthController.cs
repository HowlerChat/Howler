// <copyright file="QRAuthController.cs" company="Howler Team">
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
    using Howler.Database.Core.Models;
    using Howler.Services.InteractionServices;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Space;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides actions to retrieve and configure Spaces.
    /// </summary>
    [ApiController]
    [Route("qrauth")]
    public class QRAuthController : ControllerBase
    {
        private static Dictionary<string, object> authStore =
            new Dictionary<string, object>();

        /// <summary>
        /// Gets.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The authstore data.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Get(string id)
        {
            if (authStore.ContainsKey(id))
            {
                return this.Ok(authStore[id]);
            }

            return this.NotFound();
        }

        /// <summary>
        /// Posts.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="data">Authstore data.</param>
        /// <returns>The authstore data.</returns>
        [HttpPost("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult Post(string id, [FromBody]object data)
        {
            authStore[id] = data;
            return this.Ok(authStore[id]);
        }
    }
}