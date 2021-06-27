namespace Howler.AuthGateway.Tests.SigningAlgorithms
{
    using System;
    using System.Security.Cryptography;
    using Howler.AuthGateway.SigningAlgorithms;
    using Howler.AuthGateway.Tests.KeyProviders;
    using Xunit;

    public class RS256SigningAlgorithmUnitTests
    {
        [Fact]
        public void RS256SigningAlgorithm_Successfully_SignsPayload()
        {
            var signingAlgo = new RS256SigningAlgorithm(new MockRSAKeyProvider());
            var payload = signingAlgo.Sign("hello world");

            Assert.NotNull(payload);
        }
    }
}