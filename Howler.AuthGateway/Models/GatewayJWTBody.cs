namespace Howler.AuthGateway.Models
{
    using System;
    using Newtonsoft.Json;

    public class GatewayJWTBody
    {
        [JsonProperty("sub")]
        public string Sub { get; set; }

        [JsonProperty("device_key")]
        public string DeviceKey { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }

        [JsonProperty("token_use")]
        public string TokenUse { get => "access"; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("auth_time")]
        public long AuthTime { get; set; }

        [JsonProperty("iss")]
        public string Issuer { get; set; }

        [JsonProperty("exp")]
        public long ExpiresAt { get; set; }

        [JsonProperty("iat")]
        public long IssuedAt { get => DateTimeOffset.UtcNow.ToUnixTimeSeconds(); }

        [JsonProperty("jti")]
        public string JTI { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}
