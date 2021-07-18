// <copyright file="FederatedAuthorizationService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Authorization
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Howler.Database;
    using Howler.Database.Indexer;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;

    /// <inheritdoc/>
    public class FederatedAuthorizationService : IAuthorizationService
    {
        private IHttpContextAccessor _contextAccessor;

        private IIndexerDatabaseContext _databaseContext;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FederatedAuthorizationService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">
        /// An injected instance of the <see cref="IHttpContextAccessor"/>.
        /// </param>
        /// <param name="databaseContext">
        /// An injected instance of the
        /// <see cref="IIndexerDatabaseContext"/>.
        /// </param>
        public FederatedAuthorizationService(
            IHttpContextAccessor httpContextAccessor,
            IIndexerDatabaseContext databaseContext)
        {
            this._contextAccessor = httpContextAccessor;
            this._databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public AuthorizedUser? User
        {
            get => this._contextAccessor.HttpContext?.User != null ?
                new AuthorizedUser(this._contextAccessor.HttpContext.User)
                : null;
        }

        /// <inheritdoc/>
        public async Task<bool> IsAuthorizedAsync(string operation)
        {
            return await Task<bool>.Run(() => this.User?.Scope
                .Split(' ').Any(
                    cv => cv == System.Environment.GetEnvironmentVariable(
                        "HOWLER_SCOPE")) ?? false);
        }
    }
}