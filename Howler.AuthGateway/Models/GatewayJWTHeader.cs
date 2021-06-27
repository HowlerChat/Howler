namespace Howler.AuthGateway.Models
{
    using Newtonsoft.Json;

    public class GatewayJWTHeader
    {
        [JsonProperty("kid")]
        public string Kid { get; set; }

        [JsonProperty("alg")]
        public string Alg { get => "RS256"; }
    }
}
