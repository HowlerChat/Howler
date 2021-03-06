// <copyright file="IEntity.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    /// <summary>
    /// Describes a basic entity contract.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IEntity<TKey>
        where TKey : class
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        TKey Key { get; }
    }
}