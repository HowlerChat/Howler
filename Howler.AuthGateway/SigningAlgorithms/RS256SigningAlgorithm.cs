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
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Performs a SHA-256 hash, then signs with RSA.
    /// </summary>
    public class RS256SigningAlgorithm : ISigningAlgorithm
    {
        private RSACryptoServiceProvider _rsa;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="RS256SigningAlgorithm"/> class.
        /// </summary>
        /// <param name="provider">the key provider.</param>
        /// <remarks>
        /// Refactor friendly: move the key into the sign method, or rename
        /// this class and interface to something more sensible.
        /// </remarks>
        public RS256SigningAlgorithm(IKeyProvider provider)
        {
            this._rsa = new RSACryptoServiceProvider();
            this._rsa.ImportFromPem(Encoding.UTF8.GetString(provider.Key));
        }

        /// <summary>
        /// Performs a SHA-256 hash, then signs with RSA.
        /// </summary>
        /// <param name="payload">
        /// The payload to sign. Will be passed as UTF-8 encoding.
        /// </param>
        /// <returns>The Base64-URL-Encoded signature payload.</returns>
        public string Sign(string payload)
        {
            var bytes = this._rsa.SignData(
                Encoding.UTF8.GetBytes(payload),
                System.Security.Cryptography.SHA256.Create());

            return Base64UrlEncoder.Encode(bytes);
        }
    }
}