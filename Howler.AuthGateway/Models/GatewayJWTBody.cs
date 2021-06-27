// <copyright file="GatewayJWTBody.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The body component of the JWT, mirroring explicitly Howler Auth
    /// expectations.
    /// </summary>
    /// <remarks>
    /// Refactor friendly: claims should be a raw dictionary, this should
    /// have basic enforcement of values we want to protect.
    /// </remarks>
    public class GatewayJWTBody
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayJWTBody"/>
        /// class.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="deviceKey">The deviceKey.</param>
        /// <param name="eventId">The eventId.</param>
        /// <param name="scope">The scope.</param>
        /// <param name="authTime">The authTime.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="expiresAt">The expiresAt.</param>
        /// <param name="jti">The jti.</param>
        /// <param name="clientId">The clientId.</param>
        /// <param name="username">The username.</param>
        public GatewayJWTBody(
            string subject,
            string deviceKey,
            string eventId,
            string scope,
            long authTime,
            string issuer,
            long expiresAt,
            string jti,
            string clientId,
            string username)
        {
            this.Subject = subject;
            this.DeviceKey = deviceKey;
            this.EventId = eventId;
            this.Scope = scope;
            this.AuthTime = authTime;
            this.Issuer = issuer;
            this.ExpiresAt = expiresAt;
            this.IssuedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.JTI = jti;
            this.ClientId = clientId;
            this.Username = username;
        }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        [JsonProperty("sub")]
        public string Subject { get; private set; }

        /// <summary>
        /// Gets the device key.
        /// </summary>
        [JsonProperty("device_key")]
        public string DeviceKey { get; private set; }

        /// <summary>
        /// Gets the event id.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; private set; }

        /// <summary>
        /// Gets the token use.
        /// </summary>
        [JsonProperty("token_use")]
        public string TokenUse { get => "access"; }

        /// <summary>
        /// Gets the scope.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; private set; }

        /// <summary>
        /// Gets the auth time.
        /// </summary>
        [JsonProperty("auth_time")]
        public long AuthTime { get; private set; }

        /// <summary>
        /// Gets the issuer.
        /// </summary>
        [JsonProperty("iss")]
        public string Issuer { get; private set; }

        /// <summary>
        /// Gets the expiration time.
        /// </summary>
        [JsonProperty("exp")]
        public long ExpiresAt { get; private set; }

        /// <summary>
        /// Gets the issued at time.
        /// </summary>
        [JsonProperty("iat")]
        public long IssuedAt { get; private set; }

        /// <summary>
        /// Gets the JTI.
        /// </summary>
        [JsonProperty("jti")]
        public string JTI { get; private set; }

        /// <summary>
        /// Gets the client id.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; private set; }
    }
}
