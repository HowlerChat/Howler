namespace Howler.AuthGateway.Tests
{
    using System;
    using Howler.AuthGateway.Models;
    using Howler.AuthGateway.SigningAlgorithms;
    using Howler.AuthGateway.Tests.KeyProviders;
    using Xunit;

    public class JWTIntegrationTests
    {
        [Fact]
        public void TestName()
        {
            var keyProvider = new MockRSAProvider();
            var signingAlgo = new RS256SigningAlgorithm(keyProvider);

            var jwt = new GatewayJWT(
                new GatewayJWTHeader("XvcI95sskH6rV+L8necLpZT9KxCji2HwjQP\\/vMBxXfo="),
                new GatewayJWTBody(
                    new Guid().ToString(),
                    "us-west-2_5ffc1614-ada4-46c0-a342-e539524b3524",
                    "d96d4c00-7280-4a70-bc9a-5bf7d9089956",
                    Guid.NewGuid().ToString(),
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    "https://gateway.howler.chat",
                    "",
                    DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeSeconds(),
                    "08f1b19b-df39-4efd-bbde-8ce9d4424252",
                    "6b75ooll3b86ugauhu22vj39ra",
                    "4369d133-a36e-4a82-905d-89f620f53061"),
                signingAlgo);
            Assert.NotNull(jwt.ToString());
        }
    }
}
