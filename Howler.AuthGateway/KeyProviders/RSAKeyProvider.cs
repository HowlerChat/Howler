// <copyright file="RSAKeyProvider.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.KeyProviders
{
    using System.Text;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Provides an RSA key from configuration.
    /// </summary>
    public class RSAKeyProvider : IKeyProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RSAKeyProvider"/>
        /// class.
        /// </summary>
        /// <param name="config">The application configuration.</param>
        public RSAKeyProvider(IConfiguration config)
        {
            this.Key = Encoding.UTF8.GetBytes(config["RSAKey"]);
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public byte[] Key { get; private set; }
    }
}