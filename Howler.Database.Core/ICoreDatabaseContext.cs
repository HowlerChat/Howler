// <copyright file="ICoreDatabaseContext.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Core
{
    using System.Linq;
    using Howler.Database.Core.Models;

    /// <summary>
    /// A simple context for accessing the data store corresponding to core
    /// services.
    /// </summary>
    public interface ICoreDatabaseContext
    {
        /// <summary>
        /// Gets the Spaces.
        /// </summary>
        IQueryable<Space> Spaces { get; }

        /// <summary>
        /// Gets the Channels.
        /// </summary>
        IQueryable<Channel> Channels { get; }

        /// <summary>
        /// Gets the ChannelMembers.
        /// </summary>
        IQueryable<ChannelMember> ChannelMembers { get; }

        /// <summary>
        /// Gets the ChannelMemberStates.
        /// </summary>
        IQueryable<ChannelMemberState> ChannelMemberStates { get; }

        /// <summary>
        /// Gets the Messages.
        /// </summary>
        IQueryable<Message> Messages { get; }

        /// <summary>
        /// Gets the MessageIndex.
        /// </summary>
        IQueryable<MessageIndex> MessageIndex { get; }

        /// <summary>
        /// Gets the SpaceBans.
        /// </summary>
        IQueryable<SpaceBan> SpaceBans { get; }

        /// <summary>
        /// Gets the SpaceHistory.
        /// </summary>
        IQueryable<SpaceHistory> SpaceHistory { get; }

        /// <summary>
        /// Gets the SpaceMembers.
        /// </summary>
        IQueryable<SpaceMember> SpaceMembers { get; }
    }
}