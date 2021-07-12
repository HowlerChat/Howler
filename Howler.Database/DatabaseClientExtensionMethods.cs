// <copyright file="DatabaseClientExtensionMethods.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using Cassandra.Data.Linq;

    /// <summary>
    /// Basic extension methods to support set operations for the
    /// IDatabaseClient implementations.
    /// </summary>
    public static class DatabaseClientExtensionMethods
    {
        /// <summary>
        /// Adds an item to the table.
        /// </summary>
        /// <param name="table">The table to add to.</param>
        /// <param name="item">The item to add.</param>
        /// <typeparam name="T">The table type.</typeparam>
        /// <typeparam name="TKey">The key type.</typeparam>
        public static void Add<T, TKey>(
            this IQueryable<T> table,
            T item)
            where T : IEntity<TKey>
            where TKey : class
        {
            if (table is Table<T>)
            {
                var cqlTable = table as Table<T>;

                cqlTable!.Insert(item).Execute();
            }
            else if (table is List<T>)
            {
                var listTable = table as List<T>;

                listTable!.Add(item);
            }
        }

        /// <summary>
        /// Updates an item on the table.
        /// </summary>
        /// <param name="table">The table to update.</param>
        /// <param name="item">The item to update.</param>
        /// <typeparam name="T">The table type.</typeparam>
        /// <typeparam name="TKey">The key type.</typeparam>
        public static void Update<T, TKey>(
            this IQueryable<T> table,
            T item)
            where T : IEntity<TKey>
            where TKey : class
        {
            if (table is Table<T>)
            {
                var cqlTable = table as Table<T>;

                cqlTable!.Where(t => t.Key == item.Key).Update().Execute();
            } // no-op for lists, lists are already referentially-bound.
        }

        /// <summary>
        /// Removes an item from the table.
        /// </summary>
        /// <param name="table">The table to remove from.</param>
        /// <param name="item">The item to remove.</param>
        /// <typeparam name="T">The table type.</typeparam>
        /// <typeparam name="TKey">The key type.</typeparam>
        public static void Remove<T, TKey>(
            this IQueryable<T> table,
            T item)
            where T : IEntity<TKey>
            where TKey : class
        {
            if (table is Table<T>)
            {
                var cqlTable = table as Table<T>;

                cqlTable!.Where(t => t.Key == item.Key).Delete().Execute();
            }
            else if (table is List<T>)
            {
                var listTable = table as List<T>;

                listTable!.Remove(item);
            }
        }
    }
}