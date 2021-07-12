// <copyright file="IAttachmentService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Attachments
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a basic contract for retrieving and uploading attachments.
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Retrieves an attachment url by its id.
        /// </summary>
        /// <param name="attachmentId">The attachment id to look up.</param>
        /// <returns>The signed url.</returns>
        Task<string> GetAttachmentUrlAsync(string attachmentId);

        /// <summary>
        /// Uploads an attachment.
        /// </summary>
        /// <param name="attachmentId">The attachment id to store for.</param>
        /// <param name="file">The file stream to upload.</param>
        /// <returns>The signed url.</returns>
        Task<string> PutAttachmentAsync(string attachmentId, Stream file);
    }
}