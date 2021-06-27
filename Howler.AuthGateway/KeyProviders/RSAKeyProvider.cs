namespace Howler.AuthGateway.KeyProviders
{
    using Microsoft.Extensions.Configuration;

    public class RSAKeyProvider : IKeyProvider
    {
        public RSAKeyProvider(IConfiguration config)
        {
            this.Key = config["RSAKey"];
        }

        public string Key { get; private set; }
    }
}