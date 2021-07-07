// <copyright file="TokenRequest.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.Models
{
    /// <summary>
    /// Describes a request for a token, given a server id.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Gets or sets the server id.
        /// </summary>
        public string? ServerId { get; set; }
    }
}