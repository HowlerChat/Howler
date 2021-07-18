// <copyright file="UserProfile.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Indexer.Models
{
    using System;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The UserProfile data model.
    /// </summary>
    [Table("howler_indexer.user_profiles")]
    public class UserProfile : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Column("display_name")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the optional status.
        /// </summary>
        [Column("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the optional status emoji.
        /// </summary>
        [Column("status_emoji")]
        public string? StatusEmoji { get; set; }

        /// <summary>
        /// Gets or sets the optional icon url.
        /// </summary>
        [Column("icon_url")]
        public string? IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the optional banner url.
        /// </summary>
        [Column("banner_url")]
        public string? BannerUrl { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the modification date.
        /// </summary>
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the profile data.
        /// </summary>
        [Column("profile_data")]
        public byte[]? ProfileData { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public string Key
        {
            get => this.UserId!;
        }
    }
}