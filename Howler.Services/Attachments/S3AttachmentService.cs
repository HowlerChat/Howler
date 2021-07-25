// <copyright file="S3AttachmentService.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Attachments
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Amazon.S3;
    using Newtonsoft.Json;

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
        public async Task<string> GetAttachmentUrlAsync(string attachmentId)
        {
            var host = Environment.GetEnvironmentVariable("HOWLER_FILE_HOST");
            return await Task.FromResult($"{host}/attachments/{attachmentId}");
        }

        /// <inheritdoc/>
        public async Task<string> StageAttachmentAsync(
            string attachmentId,
            Stream file)
        {
            var bucket = Environment
                .GetEnvironmentVariable("HOWLER_FILE_BUCKET");

            var result = await this._s3Client.PutObjectAsync(
                new Amazon.S3.Model.PutObjectRequest
                {
                    InputStream = file,
                    BucketName = bucket,
                    Key = "uncommitted/" + attachmentId,
                });

            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var host = Environment
                    .GetEnvironmentVariable("HOWLER_FILE_HOST");
                return $"{host}/uncommitted/{attachmentId}";
            }

            throw new InvalidDataException(
                $"S3 returned ({result.HttpStatusCode}): " +
                JsonConvert.SerializeObject(result.ResponseMetadata));
        }

        /// <inheritdoc/>
        public async Task<string> CommitAttachmentAsync(string attachmentId)
        {
            var bucket = Environment
                .GetEnvironmentVariable("HOWLER_FILE_BUCKET");

            var result = await this._s3Client.CopyObjectAsync(
                new Amazon.S3.Model.CopyObjectRequest
                {
                    SourceBucket = bucket,
                    DestinationBucket = bucket,
                    SourceKey = "uncommitted/" + attachmentId,
                    DestinationKey = "attachments/" + attachmentId,
                });

            if (result.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                var host = Environment
                    .GetEnvironmentVariable("HOWLER_FILE_HOST");
                return $"{host}/attachments/{attachmentId}";
            }

            throw new InvalidDataException(
                $"S3 returned ({result.HttpStatusCode}): " +
                JsonConvert.SerializeObject(result.ResponseMetadata));
        }
    }
}