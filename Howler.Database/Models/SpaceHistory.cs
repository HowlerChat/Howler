// <copyright file="SpaceHistory.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Database.Models
{
    using System;
    using Cassandra.Mapping.Attributes;

    /// <summary>
    /// The SpaceHistory data model.
    /// </summary>
    [Table("space_history")]
    public class SpaceHistory : IEntity<Tuple<string, DateTime>>
    {
        /// <summary>
        /// Gets or sets the space identifier.
        /// </summary>
        [Column("space_id")]
        public string? SpaceId { get; set; }

        /// <summary>
        /// Gets or sets the change date.
        /// </summary>
        [Column("change_date")]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the id of the member performing the change.
        /// </summary>
        [Column("user_id")]
        public string? UserId { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the content of the change.
        /// </summary>
        [Column("change")]
        public byte[]? Change { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public Tuple<string, DateTime> Key
        {
            get => new Tuple<string, DateTime>(
                this.SpaceId!,
                this.ChangeDate);
        }
    }
}