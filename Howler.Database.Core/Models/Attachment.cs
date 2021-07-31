// <copyright file="Attachment.cs" company="Howler Team">
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
    /// The Attachment data model.
    /// </summary>
    [Table("howler.attachments")]
    public class Attachment : IEntity<string>
    {
        /// <summary>
        /// Gets or sets the attachment identifier.
        /// </summary>
        [Column("attachment_id")]
        public string? AttachmentId { get; set; }

        /// <summary>
        /// Gets or sets the identifer of the member who uploaded the
        /// attachment.
        /// </summary>
        [Column("member_id")]
        public string? MemberId { get; set; }

        /// <summary>
        /// Gets or sets the attachment's upload date.
        /// </summary>
        [Column("upload_date")]
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [Column("file_name")]
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        [Column("file_type")]
        public string? FileType { get; set; }

        /// <summary>
        /// Gets or sets the file size.
        /// </summary>
        [Column("file_size")]
        public int FileSize { get; set; }

#pragma warning disable SA1011
        /// <summary>
        /// Gets or sets the attachment metadata.
        /// </summary>
        [Column("metadata")]
        public byte[]? Metadata { get; set; }
#pragma warning restore SA1011

        /// <inheritdoc/>
        public string Key { get => this.AttachmentId!; }
    }
}