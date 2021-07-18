// <copyright file="SpaceInteractionServiceUnitTests.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Howler.Services.InteractionServices;
    using Howler.Services.Tests.Authorization;
    using Microsoft.Extensions.Logging;
    using Xunit;

    /// <summary>
    /// Defines the unit tests for the SpaceInteractionService.
    /// </summary>
    public class SpaceInteractionServiceUnitTests
    {

        private static MockAuthorizationService validAuthorizationService =
            new MockAuthorizationService(
                true,
                new Services.Authorization.AuthorizedUser(
                    new ClaimsPrincipal(
                        new List<ClaimsIdentity>
                        {
                            new ClaimsIdentity(new List<Claim>
                            {
                                new Claim("sub", Guid.NewGuid().ToString()),
                                new Claim(
                                    "device_key",
                                    Guid.NewGuid().ToString()),
                                new Claim(
                                    "event_id",
                                    Guid.NewGuid().ToString()),
                                new Claim("scope", "openid"),
                                new Claim(
                                    "auth_time", 
                                    DateTimeOffset.UtcNow
                                        .ToUnixTimeSeconds().ToString()),
                                new Claim(
                                    "iat", 
                                    DateTimeOffset.UtcNow
                                        .ToUnixTimeSeconds().ToString()),
                                new Claim("aud", "https://howler.chat"),
                                new Claim(
                                    "exp", 
                                    DateTimeOffset.UtcNow.AddDays(1)
                                        .ToUnixTimeSeconds().ToString()),
                                new Claim(
                                    "iss",
                                    "https://gateway.howler.chat"),
                                new Claim("jti", Guid.NewGuid().ToString()),
                                new Claim(
                                    "client_id",
                                    Guid.NewGuid().ToString()),
                                new Claim(
                                    "username",
                                    Guid.NewGuid().ToString()),
                            })
                        }
                    )));

        /// <summary>
        /// SpaceInteractionService will authorize creation when tokens are
        /// valid.
        /// </summary>
        [Fact]
        public async Task
            SpaceInteractionServiceAuthorizesCreationWithValidTokens()
        {
            var spaceInteractionService = new SpaceInteractionService(
                LoggerFactory.Create((config) => {})
                    .CreateLogger<SpaceInteractionService>(),
                null!, // TODO: add mocks for IConfig/IndexerDBC
                null!,
                validAuthorizationService);

            // actions
            var result = await spaceInteractionService.CreateSpaceAsync(
                new Models.V1.Space.CreateOrUpdateSpaceRequest
                {
                    SpaceId = "asdf"
                });

            // assertions
            Assert.Null(result.Right?.PropertyErrorCode);
        }

        /// <summary>
        /// SpaceInteractionService will not authorize creation when tokens are
        /// invalid.
        /// </summary>
        [Fact]
        public async Task
            SpaceInteractionServiceReturnsAuthFailureWithInvalidTokens()
        {
            // setup
            var invalidAuthorizationService =
                new MockAuthorizationService(false, null);
            var spaceInteractionService = new SpaceInteractionService(
                LoggerFactory.Create((config) => {})
                    .CreateLogger<SpaceInteractionService>(),
                null!, // TODO: add mocks for IConfig/IndexerDBC
                null!,
                invalidAuthorizationService);

            // actions
            var result = await spaceInteractionService.CreateSpaceAsync(
                new Models.V1.Space.CreateOrUpdateSpaceRequest
                {
                    SpaceId = "asdf"
                });

            // assertions
            Assert.Equal("INVALID_SCOPE", result.Right?.PropertyErrorCode);
        }
    }
}
