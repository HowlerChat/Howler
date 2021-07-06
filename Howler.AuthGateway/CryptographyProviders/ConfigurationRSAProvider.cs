// <copyright file="ConfigurationRSAProvider.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.CryptographyProviders
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Provides RSA from configuration.
    /// </summary>
    public class ConfigurationRSAProvider : IRSAProvider
    {
        private RSACryptoServiceProvider _rsa;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ConfigurationRSAProvider"/> class.
        /// </summary>
        /// <param name="config">The application configuration.</param>
        public ConfigurationRSAProvider(IConfiguration config)
        {
            this._rsa = new RSACryptoServiceProvider();
            this._rsa.ImportFromPem(config["RSAKey"]);
        }

        /// <inheritdoc/>
        public async Task<RSAParameters> GetPublicKeyAsync()
        {
            return await Task.Run(() => this._rsa.ExportParameters(false));
        }

        /// <inheritdoc/>
        public async Task<byte[]> SignAsync(
            byte[] payload,
            SignatureAlgorithm signatureAlgorithm)
        {
            var hashAlgorithm =
                signatureAlgorithm == SignatureAlgorithm.RS256 ?
                    System.Security.Cryptography.SHA256.Create() :
                    throw new InvalidOperationException(
                            $"Signature algorithm {signatureAlgorithm}" +
                            " unsupported.");

            return await Task.Run(
                () => this._rsa.SignData(
                    payload,
                    hashAlgorithm));
        }
    }
}