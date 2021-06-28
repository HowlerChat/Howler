// <copyright file="OpenIDConfigurationInfo.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes an OpenID Configuration document.
    /// </summary>
    public class OpenIDConfigurationInfo
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="OpenIDConfigurationInfo"/> class.
        /// </summary>
        /// <remarks>
        /// Refactor friendly: pull this from config info.
        /// </remarks>
        public OpenIDConfigurationInfo()
        {
            this.AuthorizationEndpoint =
                "https://auth.howler.chat/oauth2/authorize";
            this.IDTokenSigningAlgorithmValuesSupported = new string[]
            {
                "RS256",
            };
            this.Issuer = "https://gateway.howler.chat";
            this.JwksUri = "https://gateway.howler.chat/.well-known/jwks.json";
            this.ResponseTypesSupported = new string[] { "code", "token" };
            this.ScopesSupported = new string[]
            {
                "openid",
                "email",
                "phone",
                "profile",
            };
            this.SubjectTypesSupported = new string[] { "public" };
            this.TokenEndpoint = "https://auth.howler.chat/oauth2/token";
            this.TokenEndpointAuthMethodsSupported = new string[]
            {
                "client_secret_basic",
                "client_secret_post",
            };
            this.UserinfoEndpoint = "https://auth.howler.chat/oauth2/userInfo";
        }

        /// <summary>
        /// Gets the authorization endpoint.
        /// </summary>
        [JsonProperty("authorization_endpoint")]
        public string AuthorizationEndpoint { get; private set; }

        /// <summary>
        /// Gets the id token signing alg values supported.
        /// </summary>
        [JsonProperty("id_token_signing_alg_values_supported")]
        public string[] IDTokenSigningAlgorithmValuesSupported
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the issuer.
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; private set; }

        /// <summary>
        /// Gets the jwks uri.
        /// </summary>
        [JsonProperty("jwks_uri")]
        public string JwksUri { get; private set; }

        /// <summary>
        /// Gets the response types supported.
        /// </summary>
        [JsonProperty("response_types_supported")]
        public string[] ResponseTypesSupported { get; private set; }

        /// <summary>
        /// Gets the scopes supported.
        /// </summary>
        [JsonProperty("scopes_supported")]
        public string[] ScopesSupported { get; private set; }

        /// <summary>
        /// Gets the subject types supported.
        /// </summary>
        [JsonProperty("subject_types_supported")]
        public string[] SubjectTypesSupported { get; private set; }

        /// <summary>
        /// Gets the token endpoint.
        /// </summary>
        [JsonProperty("token_endpoint")]
        public string TokenEndpoint { get; private set; }

        /// <summary>
        /// Gets the token endpoint auth methods supported.
        /// </summary>
        [JsonProperty("token_endpoint_auth_methods_supported")]
        public string[] TokenEndpointAuthMethodsSupported { get; private set; }

        /// <summary>
        /// Gets the userinfo endpoint.
        /// </summary>
        [JsonProperty("userinfo_endpoint")]
        public string UserinfoEndpoint { get; private set; }
    }
}