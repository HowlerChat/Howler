// <copyright file="OpenIDConfigurationController.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Controllers
{
    using System;
    using System.Linq;
    using Howler.AuthGateway.Models;
    using Howler.Database;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// OpenID Configuration Controller.
    /// </summary>
    [Route(".well-known")]
    [ApiController]
    public class OpenIDConfigurationController : ControllerBase
    {
        private IRSAProvider _provider;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="OpenIDConfigurationController"/> class.
        /// </summary>
        /// <param name="provider">
        /// An injected instance of the key provider.
        /// </param>
        /// <remarks>
        /// Refactor friendly - needs support for additional key providers.
        /// </remarks>
        public OpenIDConfigurationController(IRSAProvider provider)
        {
            this._provider = provider;
        }

        /// <summary>
        /// Retrieves openid-configuration info.
        /// </summary>
        /// <returns>
        /// Returns openid-configuration info.
        /// </returns>
        [HttpGet("openid-configuration")]
        [ProducesResponseType(typeof(OpenIDConfigurationInfo), 200)]
        public OpenIDConfigurationInfo Config()
        {
            return new OpenIDConfigurationInfo();
        }

        /// <summary>
        /// Retrieves JWKS info.
        /// </summary>
        /// <returns>
        /// Returns JWKS info.
        /// </returns>
        [HttpGet("jwks.json")]
        [ProducesResponseType(typeof(JWKSInfo), 200)]
        public JWKSInfo JWKS()
        {
            return new JWKSInfo(this._provider);
        }
    }
}
