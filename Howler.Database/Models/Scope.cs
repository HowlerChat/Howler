// <copyright file="Scope.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using System;

    /// <summary>
    /// Defines a hosting scope for federation.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Gets or sets the scope identifier hash.
        /// </summary>
        public string? ScopeId { get; set; }

        /// <summary>
        /// Gets or sets the scope name.
        /// </summary>
        public string? ScopeName { get; set; }

        /// <summary>
        /// Gets or sets the scope server uri.
        /// </summary>
        public string? ScopeServerUri { get; set; }

        /// <summary>
        /// Gets or sets the scope owner identifier.
        /// </summary>
        public string? ScopeOwnerId { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the scope termination date.
        /// </summary>
        public DateTime? TerminationDate { get; set; }
    }
}