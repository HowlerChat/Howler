// <copyright file="ChannelGroupResponse.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Space
{
    using System.Collections.Generic;

    /// <summary>
    /// A collection of channel groups.
    /// </summary>
    public class ChannelGroupResponse : Dictionary<string, string[]>
    {
    }
}