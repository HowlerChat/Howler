// <copyright file="S3AttachmentService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Attachments
{
    using System.IO;
    using System.Threading.Tasks;
    using Amazon.S3;

    /// <summary>
    /// Provides attachment services using S3 as the storage layer.
    /// </summary>
    public class S3AttachmentService : IAttachmentService
    {
        private IAmazonS3 _s3Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="S3AttachmentService"/>
        /// class.
        /// </summary>
        /// <param name="s3Client">
        /// An injected instance of the s3 client.
        /// </param>
        public S3AttachmentService(IAmazonS3 s3Client)
        {
            this._s3Client = s3Client;
        }

        /// <inheritdoc/>
        public Task<string> GetAttachmentUrlAsync(string attachmentId)
        {
            throw new System.NotSupportedException();
        }

        /// <inheritdoc/>
        public Task<string> PutAttachmentAsync(
            string attachmentId,
            Stream file)
        {
            throw new System.NotSupportedException();
        }
    }
}