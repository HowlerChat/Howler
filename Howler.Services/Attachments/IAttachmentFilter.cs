// <copyright file="IAttachmentFilter.cs" company="Howler Team">
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
    /// Provides a basic contract for filtering attachments.
    /// </summary>
    public interface IAttachmentFilter
    {
        /// <summary>
        /// Evaluates an attachment for forbidden content. If acceptable,
        /// returns true.
        /// </summary>
        /// <param name="attachmentId">The attachment identifier.</param>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="file">The file stream.</param>
        /// <returns>True if permitted.</returns>
        Task<bool> EvaluateAttachment(
            string attachmentId,
            string memberId,
            Stream file);
    }
}