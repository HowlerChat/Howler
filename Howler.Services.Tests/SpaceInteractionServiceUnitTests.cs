// <copyright file="SpaceInteractionServiceUnitTests.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Tests
{
    using System;
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
        /// <summary>
        /// SpaceInteractionService will authorize creation when tokens are
        /// valid.
        /// </summary>
        [Fact]
        public async Task
            SpaceInteractionServiceAuthorizesCreationWithValidTokens()
        {
            // setup
            var validAuthorizationService = new MockAuthorizationService(true);
            var spaceInteractionService = new SpaceInteractionService(
                LoggerFactory.Create((config) => {})
                    .CreateLogger<SpaceInteractionService>(),
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
                new MockAuthorizationService(false);
            var spaceInteractionService = new SpaceInteractionService(
                LoggerFactory.Create((config) => {})
                    .CreateLogger<SpaceInteractionService>(),
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
