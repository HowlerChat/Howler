// <copyright file="AuthorizedUser.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Authorization
{
    using System.Security.Claims;
    using Newtonsoft.Json;

    /// <summary>
    /// An authorized user.
    /// </summary>
    public class AuthorizedUser
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AuthorizedUser"/> class.
        /// </summary>
        /// <param name="principal">
        /// A claims principal as provided by the JWT.
        /// </param>
        public AuthorizedUser(ClaimsPrincipal principal)
        {
            this.Subject = principal.FindFirstValue("sub") ??
                principal.FindFirstValue(
                    "http://schemas.xmlsoap.org/ws/2005/05/" +
                    "identity/claims/nameidentifier");
            this.DeviceKey = principal.FindFirstValue("device_key");
            this.EventId = principal.FindFirstValue("event_id");
            this.Scope = principal.FindFirstValue("scope");
            this.AuthTime = long.Parse(principal.FindFirstValue("auth_time"));
            this.Issuer = principal.FindFirstValue("iss");
            this.Audience = principal.FindFirstValue("aud");
            this.ExpiresAt = long.Parse(principal.FindFirstValue("exp"));
            this.IssuedAt = long.Parse(principal.FindFirstValue("iat"));
            this.JTI = principal.FindFirstValue("jti");
            this.ClientId = principal.FindFirstValue("client_id");
            this.Username = principal.FindFirstValue("username");
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
        /// Gets the audience.
        /// </summary>
        [JsonProperty("aud")]
        public string Audience { get; private set; }

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