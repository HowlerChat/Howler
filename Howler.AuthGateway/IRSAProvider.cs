// <copyright file="IRSAProvider.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway
{
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Howler.AuthGateway.CryptographyProviders;

    /// <summary>
    /// Provides cryptographic services using RSA.
    /// </summary>
    public interface IRSAProvider
    {
        /// <summary>
        /// Retrieves public key parameters.
        /// </summary>
        /// <returns>RSA public key.</returns>
        Task<RSAParameters> GetPublicKeyAsync();

        /// <summary>
        /// Signs a payload hashed by the specified hash algorithm.
        /// </summary>
        /// <param name="payload">The payload to sign.</param>
        /// <param name="signatureAlgorithm">
        /// The constructed hash algorithm to use.
        /// </param>
        /// <returns>The signed payload.</returns>
        Task<byte[]> SignAsync(
            byte[] payload,
            SignatureAlgorithm signatureAlgorithm);
    }
}