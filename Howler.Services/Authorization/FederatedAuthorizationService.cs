// <copyright file="FederatedAuthorizationService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.InteractionServices
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Threading.Tasks;
    using Howler.Database;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;

    /// <inheritdoc/>
    public class FederatedAuthorizationService : IAuthorizationService
    {
        private IHttpContextAccessor _contextAccessor;

        private IFederationDatabaseContext _databaseContext;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="FederatedAuthorizationService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">
        /// An injected instance of the <see cref="IHttpContextAccessor"/>.
        /// </param>
        /// <param name="databaseContext">
        /// An injected instance of the
        /// <see cref="IFederationDatabaseContext"/>.
        /// </param>
        public FederatedAuthorizationService(
            IHttpContextAccessor httpContextAccessor,
            IFederationDatabaseContext databaseContext)
        {
            this._contextAccessor = httpContextAccessor;
            this._databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public async Task<bool> IsAuthorizedAsync(string operation)
        {
            return await Task<bool>.Run(() => this._contextAccessor.HttpContext?
                .User.Claims.Any(c => c.Type == "scope" &&
                    (c.Value?.Split(' ').Any(
                        cv => cv == System.Environment.GetEnvironmentVariable(
                            "HOWLER_SCOPE")) ?? false)) ?? false);
        }
    }
}