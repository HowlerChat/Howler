// <copyright file="IndexInsertResponse.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Search
{
    /// <summary>
    /// Describes the response when inserting an item into an index.
    /// </summary>
    public class IndexInsertResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexInsertResponse"/>
        /// class.
        /// </summary>
        /// <param name="indexName">The index name.</param>
        /// <param name="type">The type indexed.</param>
        /// <param name="identifier">The identifier of the indexed item.</param>
        /// <param name="status">The status of the indexed item.</param>
        public IndexInsertResponse(
            string indexName,
            string type,
            string identifier,
            string status) =>
            (this.IndexName, this.Type, this.Identifier, this.Status) =
            (indexName, type, identifier, status);

        /// <summary>
        /// Gets the name of the index.
        /// </summary>
        public string IndexName { get; }

        /// <summary>
        /// Gets the type of the indexed item.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the identifier of the indexed item.
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Gets the status of the indexed item.
        /// </summary>
        public string Status { get; }
    }
}
