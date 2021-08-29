// <copyright file="ISearchIndexerService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Search
{
    using System.Threading.Tasks;
    using Howler.Services.Models;
    using Howler.Services.Models.V1.Errors;
    using Howler.Services.Models.V1.Search;

    /// <summary>
    /// Defines a contract for interacting with a search index.
    /// </summary>
    public interface ISearchIndexerService
    {
        /// <summary>
        /// Adds an item to a search index.
        /// </summary>
        /// <param name="indexName">The name of the index.</param>
        /// <param name="type">The item's type.</param>
        /// <param name="identifier">The item's identifier.</param>
        /// <param name="item">The item to index.</param>
        /// <returns>Either an indexing response or an error.</returns>
        Task<Either<ErrorResponse, IndexInsertResponse>>
            AddToSearchIndexAsync(
                string indexName,
                string type,
                string identifier,
                object item);
    }
}
