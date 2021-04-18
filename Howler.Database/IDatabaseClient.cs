// <copyright file="IDatabaseClient.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database
{
    using System;
    using Cassandra;

    /// <summary>
    /// Defines a contract for accessing a Cassandra session object.
    /// </summary>
    public interface IDatabaseClient
    {
        /// <summary>
        /// Gets the Session object for the Cassandra server.
        /// </summary>
        ISession Session { get; }
    }
}
