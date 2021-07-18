// <copyright file="SpaceMember.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Core.Models
{
    using System;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The SpaceMember data model.
    /// </summary>
    [Table("howler.space_members")]
    public class SpaceMember : IEntity<Tuple<string, string>>
    {
        /// <summary>
        /// Gets or sets the space id.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the member id. Member identifiers differ from standard
        /// user identifiers in that they can be indirect proxies for user info
        /// in the form of a user id (guid) or a identity key thumbprint
        /// (hash).
        /// </summary>
        [Column("member_id")]
        public string? MemberId { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the profile data, representing a unique profile as
        /// appears within a space. When omitted and MemberId is a regular
        /// user_id, the default user profile data is displayed.
        /// </summary>
        [Column("profile_data")]
        public byte[]? ProfileData { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public Tuple<string, string> Key
        {
            get => new Tuple<string, string>(
                this.SpaceId!,
                this.MemberId!);
        }
    }
}