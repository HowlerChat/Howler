// <copyright file="GatewayJWT.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Models
{
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    /// <summary>
    /// Describes a JWT issued by the Gateway Auth Server.
    /// </summary>
    public class GatewayJWT
    {
        private ISigningAlgorithm _algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayJWT"/>
        /// class.
        /// </summary>
        /// <param name="header">The header of the JWT.</param>
        /// <param name="body">The body of the JWT.</param>
        /// <param name="algorithm">The signing algorithm.</param>
        /// <remarks>
        /// Refactor friendly: move signing algorithm out, turn into
        /// JsonConverter.
        /// </remarks>
        public GatewayJWT(
            GatewayJWTHeader header,
            GatewayJWTBody body,
            ISigningAlgorithm algorithm)
        {
            this.Header = header;
            this.Body = body;
            this._algorithm = algorithm;
        }

        /// <summary>
        /// Gets the header of the JWT.
        /// </summary>
        public GatewayJWTHeader Header { get; private set; }

        /// <summary>
        /// Gets the body of the JWT.
        /// </summary>
        public GatewayJWTBody Body { get; private set; }

        /// <summary>
        /// Serializes the JWT into the common encoding.
        /// </summary>
        /// <returns>Returns the serialized JWT.</returns>
        public override string ToString()
        {
            var payload = Base64UrlEncoder.Encode(
                    JsonConvert.SerializeObject(this.Header)) + "." +
                Base64UrlEncoder.Encode(
                    JsonConvert.SerializeObject(this.Body));

            return payload + "." + this._algorithm.Sign(payload);
        }
    }
}
