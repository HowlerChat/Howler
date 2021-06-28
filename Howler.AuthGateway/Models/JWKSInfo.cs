// <copyright file="JWKSInfo.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes JWKS Key Info.
    /// </summary>
    public class JWKSInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JWKSInfo"/> class.
        /// </summary>
        /// <remarks>
        /// Refactor friendly: pull this from config info.
        /// </remarks>
        public JWKSInfo()
        {
            this.Keys = new object[]
            {
                new
                {
                    Alg = "RS256",
                    E = "AQAB",
                    Kid = "XvcI95sskH6rV+L8necLpZT9KxCji2HwjQP\\/vMBxXfo=",
                    Kty = "RSA",
                    N = "xKGrA9YbPpcmRxuITT256h2uY4A-1NjegGHZu2d_lBXq3Hpd9Lu" +
                        "9_nwvYGgp8F692Yn3Ef1ySu3SESy7hdM5MMY0jW1ZVN4BjHmlKW" +
                        "9O0pKr_9qtCNTThdO1c7zvZoZ_J_KScNFGVZ87oY0IBSPaz66pS" +
                        "8c18aStnS-_CPcqv4GzIoZRkxfZrJZsDSA6YaahWPLCrfFTZtqY" +
                        "ZpAgdUw1pNyIjrO2hQ_vJ2W5SsCB4c0KxScVpPwKYGhvRi3XlKB" +
                        "b7b91_7nThd5FGo-miklbWRxslORlDb8RxNWzcdjo_zjTrItz5W" +
                        "UzO8Q4PuzHIzorMs91OLBTKCOry2WeENcPRQ",
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