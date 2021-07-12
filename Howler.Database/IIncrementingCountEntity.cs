// <copyright file="IIncrementingCountEntity.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    /// <summary>
    /// Describes an entity contract with an incremental counter side table.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IIncrementingCountEntity<TKey> : IEntity<TKey>
        where TKey : class
    {
        /// <summary>
        /// Gets the name of the supplemental counter table.
        /// </summary>
        string CounterTable { get; }

        /// <summary>
        /// Gets the name of the counter column.
        /// </summary>
        string CounterColumn { get; }
    }
}