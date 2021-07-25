// <copyright file="FileAttachmentService.cs" company="Howler Team">
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

    /// <summary>
    /// Provides attachment services using the local filesystem as the storage
    /// layer.
    /// </summary>
    public class FileAttachmentService : IAttachmentService
    {
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
            var host = Environment.GetEnvironmentVariable("HOWLER_FILE_HOST");

            using (var newFile = File.
                Create($"{host}/uncomitted/{attachmentId}"))
            {
                await file.CopyToAsync(newFile);
            }

            return await Task.FromResult($"{host}/uncomitted/{attachmentId}");
        }

        /// <inheritdoc/>
        public async Task<string> CommitAttachmentAsync(string attachmentId)
        {
            var host = Environment.GetEnvironmentVariable("HOWLER_FILE_HOST");

            File.Move(
                $"{host}/uncomitted/{attachmentId}",
                $"{host}/attachments/{attachmentId}");

            return await this.GetAttachmentUrlAsync(attachmentId);
        }
    }
}