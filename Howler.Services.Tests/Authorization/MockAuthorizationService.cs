// <copyright file="MockAuthorizationService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Tests.Authorization
{
    using System.Threading.Tasks;
    using Howler.Services.Authorization;

    /// <summary>
    /// Defines a simple authoriztion service with control of authorization
    /// based on only initialization.
    /// </summary>
    public class MockAuthorizationService : IAuthorizationService
    {
        private bool _isAuthorized = true;

        private AuthorizedUser? _authorizedUser = null;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MockAuthorizationService"/> class.
        /// </summary>
        /// <param name="isAuthorized">
        /// A boolean value indicating whether or not calls will be
        /// authorized.
        /// </param>
        /// <param name="authorizedUser">
        /// The authorized user to mock out.
        /// </param>
        public MockAuthorizationService(
            bool isAuthorized,
            AuthorizedUser? authorizedUser) =>
            (this._isAuthorized, this._authorizedUser) =
            (isAuthorized, authorizedUser);

        /// <inheritdoc/>
        public AuthorizedUser? User => this._authorizedUser;

        /// <inheritdoc/>
        public async Task<bool> IsAuthorizedAsync(string operation) =>
            await Task.Run(() => this._isAuthorized);
    }
}