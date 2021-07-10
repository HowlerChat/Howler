// <copyright file="IAuthorizationService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Authorization
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a simple service for handling authorization.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Gets the authorized user.
        /// </summary>
        AuthorizedUser? User { get; }

        /// <summary>
        /// Determines if the active user is authorized to perform the
        /// specified operation.
        /// </summary>
        /// <param name="operation">The name of the operation.</param>
        /// <returns>
        /// A boolean value indicating whether or not the user is authorized
        /// for the scope and operation.
        /// </returns>
        Task<bool> IsAuthorizedAsync(string operation);
    }
}