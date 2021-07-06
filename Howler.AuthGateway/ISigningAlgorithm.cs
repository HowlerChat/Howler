// <copyright file="ISigningAlgorithm.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway
{
    using System.Threading.Tasks;

    /// <summary>
    /// Describes a signing algorithm's methods.
    /// </summary>
    public interface ISigningAlgorithm
    {
        /// <summary>
        /// Performs the signing algorithm on a payload, encoding
        /// passed to the algorithm as UTF-8.
        /// </summary>
        /// <param name="payload">The UTF-8 encoded payload.</param>
        /// <returns>The Base64-URL-Encoded signature payload.</returns>
        Task<string> SignAsync(string payload);
    }
}