namespace Howler.AuthGateway.Tests.SigningAlgorithms
{
    using System.Threading.Tasks;
    using Howler.AuthGateway.SigningAlgorithms;
    using Howler.AuthGateway.Tests.KeyProviders;
    using Xunit;

    public class RS256SigningAlgorithmUnitTests
    {
        [Fact]
        public async Task RS256SigningAlgorithm_Successfully_SignsPayload()
        {
            var signingAlgo = new RS256SigningAlgorithm(new MockRSAProvider());
            var payload = await signingAlgo.SignAsync("hello world");

            Assert.NotNull(payload);
        }
    }
}