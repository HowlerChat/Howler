// <copyright file="GatewayController.cs" company="Howler Team">
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
    using Howler.Database.Indexer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Core Authorization Gateway Controller.
    /// </summary>
    [Route("oauth2")]
    [Authorize]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private ISigningAlgorithm _signingAlgorithm;

        private IIndexerDatabaseContext _indexerDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayController"/>
        /// class.
        /// </summary>
        /// <param name="indexerDatabaseContext">
        /// An injected instance of the database.
        /// </param>
        /// <param name="signingAlgorithm">
        /// An injected instance of the signing algorithm.
        /// </param>
        /// <remarks>
        /// Refactor friendly - needs support for additional signing
        /// algorithms.
        /// </remarks>
        public GatewayController(
            IIndexerDatabaseContext indexerDatabaseContext,
            ISigningAlgorithm signingAlgorithm)
        {
            this._indexerDatabaseContext = indexerDatabaseContext;
            this._signingAlgorithm = signingAlgorithm;
        }

        /// <summary>
        /// Authorizes a user for the target federated server id.
        /// </summary>
        /// <param name="request">The federated server id.</param>
        /// <returns>
        /// Returns an auth token for use with the federated server.
        /// </returns>
        [HttpPost("token")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public IActionResult Post([FromBody]TokenRequest request)
        {
            var tokens = this.HttpContext.Request.Headers["Authorization"];

            if (tokens.Count != 1 || request.ServerId == null)
            {
                // Attempt at pushing multiple tokens, kick em out.
                return this.Forbid();
            }

            var token = tokens.First().Split(' ').Last();
            var jwt = new Microsoft.IdentityModel.JsonWebTokens
                .JsonWebToken(token);
            var validatedServerId = this._indexerDatabaseContext.Servers
                .Where(s => s.ServerId == request.ServerId)
                .Select(s => s.ServerId)
                .ToList()
                .FirstOrDefault();

            if (validatedServerId == null)
            {
                return this.NotFound();
            }

            // TODO: Whitelist/blacklist
            return this.Ok(
                new GatewayJWT(
                    new GatewayJWTHeader(
                        "XvcI95sskH6rV+L8necLpZT9KxCji2HwjQP\\/vMBxXfo="),
                    new GatewayJWTBody(
                        jwt.Subject,
                        jwt.GetClaim("device_key").Value,
                        jwt.GetClaim("event_id").Value,
                        request.ServerId,
                        long.Parse(jwt.GetClaim("auth_time").Value),
                        "https://gateway.howler.chat",
                        request.ServerId,
                        DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeSeconds(),
                        jwt.GetClaim("jti").Value,
                        jwt.GetClaim("client_id").Value,
                        jwt.GetClaim("username").Value),
                    this._signingAlgorithm).ToString());
        }
    }
}
