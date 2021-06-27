// <copyright file="GatewayJWTHeader.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The header component of the JWT, mirroring explicitly Howler Auth
    /// expectations.
    /// </summary>
    /// <remarks>
    /// Refactor friendly: Alg needs to be specifiable against a constant
    /// mapping of algorithms.
    /// </remarks>
    public class GatewayJWTHeader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayJWTHeader"/>
        /// class.
        /// </summary>
        /// <param name="kid">The key id.</param>
        public GatewayJWTHeader(string kid)
        {
            this.Kid = kid;
        }

        /// <summary>
        /// Gets the key id.
        /// </summary>
        [JsonProperty("kid")]
        public string Kid { get; private set; }

        /// <summary>
        /// Gets the signing algorithm.
        /// </summary>
        [JsonProperty("alg")]
        public string Alg { get => "RS256"; }
    }
}
