// <copyright file="FileMetadataFilter.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Attachments
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FileMagic;
    using Howler.Database;
    using Howler.Database.Core;
    using Newtonsoft.Json;

    /// <summary>
    /// Retrieves metadata from the attachment and stores it.
    /// </summary>
    public class FileMetadataFilter : IAttachmentFilter
    {
        private IFileTypeDetector _fileTypeDetector;

        private ICoreDatabaseContext _coreDatabaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMetadataFilter"/>
        /// class.
        /// </summary>
        /// <param name="fileTypeDetector">
        /// An injected instance of a file type detector.
        /// </param>
        /// <param name="coreDatabaseContext">
        /// An injected instance of the core database context.
        /// </param>
        public FileMetadataFilter(
            IFileTypeDetector fileTypeDetector,
            ICoreDatabaseContext coreDatabaseContext)
        {
            this._fileTypeDetector = fileTypeDetector;
            this._coreDatabaseContext = coreDatabaseContext;
        }

        /// <inheritdoc/>
        public Task<bool> EvaluateAttachment(
            string attachmentId,
            string memberId,
            Stream file)
        {
            var attachment = this._coreDatabaseContext.Attachments
                .Where(a => a.AttachmentId == attachmentId)
                .ToList().FirstOrDefault();

            if (attachment != null)
            {
                return Task.FromResult(true);
            }

            var fileType = this._fileTypeDetector.Detect(file);

            attachment = new Database.Core.Models.Attachment
            {
                AttachmentId = attachmentId,
                MemberId = memberId,
                UploadDate = DateTime.UtcNow,
                FileName = attachmentId,
                FileType = fileType?.type,
                FileSize = (int)file.Length,
                Metadata = Encoding.UTF8.GetBytes(
                    JsonConvert.SerializeObject(
                        fileType)),
            };

            this._coreDatabaseContext.Attachments.Add<
                Database.Core.Models.Attachment, string>(attachment);

            return Task.FromResult(true);
        }
    }
}