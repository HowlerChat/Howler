// <copyright file="RS256SigningAlgorithm.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.SigningAlgorithms
{
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Howler.AuthGateway.CryptographyProviders;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Performs a SHA-256 hash, then signs with RSA.
    /// </summary>
    public class RS256SigningAlgorithm : ISigningAlgorithm
    {
        private IRSAProvider _provider;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="RS256SigningAlgorithm"/> class.
        /// </summary>
        /// <param name="provider">The rsa provider.</param>
        public RS256SigningAlgorithm(IRSAProvider provider)
        {
            this._provider = provider;
        }

        /// <summary>
        /// Performs a SHA-256 hash, then signs with RSA.
        /// </summary>
        /// <param name="payload">
        /// The payload to sign. Will be passed as UTF-8 encoding.
        /// </param>
        /// <returns>The Base64-URL-Encoded signature payload.</returns>
        public async Task<string> SignAsync(string payload)
        {
            var bytes = await this._provider.SignAsync(
                Encoding.UTF8.GetBytes(payload),
                SignatureAlgorithm.RS256);

            return Base64UrlEncoder.Encode(bytes);
        }
    }
}