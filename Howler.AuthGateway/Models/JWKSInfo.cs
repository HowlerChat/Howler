// <copyright file="JWKSInfo.cs" company="Howler Team">
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
    /// Describes JWKS Key Info.
    /// </summary>
    public class JWKSInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JWKSInfo"/> class.
        /// </summary>
        /// <param name="provider">The key provider.</param>
        /// <remarks>
        /// Refactor friendly: pull this from config info.
        /// </remarks>
        public JWKSInfo(IRSAProvider provider)
        {
            var key = provider.GetPublicKeyAsync().Result;
            this.Keys = new object[]
            {
                new
                {
                    Alg = "RS256",
                    E = "AQAB",
                    Kid = "XvcI95sskH6rV+L8necLpZT9KxCji2HwjQP\\/vMBxXfo=",
                    Kty = "RSA",
                    N = Base64UrlEncoder.Encode(key.Modulus),
                    Use = "sig",
                },
            };
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        [JsonProperty("keys")]
        public object[] Keys { get; private set; }
    }
}