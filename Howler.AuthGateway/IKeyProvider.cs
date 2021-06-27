// <copyright file="IKeyProvider.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway
{
    /// <summary>
    /// Provides a raw key in a format expected by the signing
    /// algorithm.
    /// </summary>
    public interface IKeyProvider
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        byte[] Key { get; }
    }
}