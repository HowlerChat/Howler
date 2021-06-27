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
            var keyProvider = new MockRSAKeyProvider();
            var signingAlgo = new RS256SigningAlgorithm(keyProvider);

            var jwt = new GatewayJWT(signingAlgo);
            jwt.Header = new GatewayJWTHeader
            {
                Kid = "XvcI95sskH6rV+L8necLpZT9KxCji2HwjQP\\/vMBxXfo="
            };
            jwt.Body = new GatewayJWTBody
            {
                Sub = new Guid().ToString(),
                DeviceKey = "us-west-2_5ffc1614-ada4-46c0-a342-e539524b3524",
                EventId = "d96d4c00-7280-4a70-bc9a-5bf7d9089956",
                Scope = Guid.NewGuid().ToString(),
                AuthTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                Issuer = "https://gateway.howler.chat",
                ExpiresAt = DateTimeOffset.UtcNow.AddHours(2).ToUnixTimeSeconds(),
                JTI = "08f1b19b-df39-4efd-bbde-8ce9d4424252",
                ClientId = "6b75ooll3b86ugauhu22vj39ra",
                Username = "4369d133-a36e-4a82-905d-89f620f53061"
            };
            Assert.Null(jwt.ToString());
        }
    }
}

/*
{
    "D": "DEEAeVQLgAE0LHH1vwOegkSsenSoF2U4O+unyIpeHOhIy18//23caIQDVK9Ux8g7JbGb0uyjL94v8kdIF0tuo0p3Ei/li86vflgeCaNhgsoWl3BJiRa2vFLSHqJ+INgHCG9O9lMwwcJcnv3O0VRhH2reXNACu/u+wAUtqKtjaxk=",
    "DP": "hCzbQw0Si4dexPbrUpFbW1mLZHS6iyXRglWl4VD7M0XZgaZI+dF66Y+RzpUCwOKyAPUujPJrh+UyZk2NQtc3lw==",
    "DQ": "ei4r0JwTvIQ52d4kE2mqp9C26gTRvVzZqw+pHEAZM4thKibn6rqDJQuYQ9M6oluimtAEhaO9vOBS8Scuuzyf+w==",
    "Exponent": "AQAB",
    "InverseQ": "IUzr/RinHOVYQWajo3tiqC4660Z0YyuDxXnVQgVSiuU1kXsumlBmXXSsZxS1lZFpsHYog0u063LgyQj7yc0JJQ==",
    "Modulus": "sR//rgvKef+O8VPNA1oWPWKDK1Bu5RXGBWoE+tBh+kNmLWXvcD0yKOcI9d/JS2C3RbeWjv0R+8lVgpwyDuEODMtwBPDE2AptOcboG2IbtyOeJ8OjAn+4DDnxA9xzvHlJA03NITQoS+yw4IgPD752R/tbYxPLZ9FQ13ydP/+mxzE=",
    "P": "ykrhux6BVvqXYuwcleTVdMTfUPQy3P+v5AQjaOI/q9SsY7Gy8HoVMY7A+5xcRQXk3jkpi2RBFR0upXH8sLk/nw==",
    "Q": "4CaRmFxjyYHvFpwF8d3GQ9TK8G0C46llZQg8OXtkSgRnuJE7U6ecTMjWH5l7nz5Cl45sqqUIyHga5frwlqFHLw=="
}


*/