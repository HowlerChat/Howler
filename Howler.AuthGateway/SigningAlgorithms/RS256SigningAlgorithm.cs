namespace Howler.AuthGateway.SigningAlgorithms
{
    using System.Security.Cryptography;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class RS256SigningAlgorithm : ISigningAlgorithm
    {
        private RSACryptoServiceProvider _rsa;

        public RS256SigningAlgorithm(IKeyProvider provider)
        {
            this._rsa = new RSACryptoServiceProvider();
            this._rsa.ImportFromPem(provider.Key);
            
        }

        public string Sign(string payload)
        {
            var bytes = this._rsa.SignData(
                Encoding.UTF8.GetBytes(payload),
                System.Security.Cryptography.SHA256.Create());
            
            return Base64UrlEncoder.Encode(bytes);
        }
    }
}