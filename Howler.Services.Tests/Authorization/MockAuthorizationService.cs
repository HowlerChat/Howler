// <copyright file="MockAuthorizationService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Tests.Authorization
{
    using System.Threading.Tasks;
    using Howler.Services.InteractionServices;

    /// <summary>
    /// Defines a simple authoriztion service with control of authorization
    /// based on only initialization.
    /// </summary>
    public class MockAuthorizationService : IAuthorizationService
    {
        private bool _isAuthorized = true;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MockAuthorizationService"/> class.
        /// </summary>
        /// <param name="isAuthorized">
        /// A boolean value indicating whether or not calls will be
        /// authorized.
        /// </param>
        public MockAuthorizationService(bool isAuthorized) =>
            this._isAuthorized = isAuthorized;

        /// <inheritdoc/>
        public async Task<bool> IsAuthorizedAsync(string operation) =>
            await Task.Run(() => this._isAuthorized);
    }
}