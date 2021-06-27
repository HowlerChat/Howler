namespace Howler.AuthGateway.Models
{
    using System;
    using Microsoft.IdentityModel.Tokens;
    using Newtonsoft.Json;

    public class GatewayJWT
    {
        private ISigningAlgorithm _algorithm;

        public GatewayJWT(ISigningAlgorithm algorithm)
        {
            this._algorithm = algorithm;
        }

        public GatewayJWTHeader Header { get; set; }

        public GatewayJWTBody Body { get; set; }

        public override string ToString()
        {
            var payload = Base64UrlEncoder.Encode(
                JsonConvert.SerializeObject(this.Header)
            ) + "." + Base64UrlEncoder.Encode(
                JsonConvert.SerializeObject(this.Body)
            );
            
            return payload + "." + this._algorithm.Sign(payload);
        }
    }
}
