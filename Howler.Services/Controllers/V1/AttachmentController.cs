// <copyright file="AttachmentController.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Howler.Services.Attachments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides actions to create and retrieve attachments.
    /// </summary>
    [ApiController]
    [Route("v1/attachments")]
    [Authorize]
    public class AttachmentController : ControllerBase
    {
        private static Regex attachmentIdFilter =
            new Regex("^[a-fA-F0-9]+$");

        private ILogger<AttachmentController> _logger;

        private IAttachmentService _attachmentService;

        private IEnumerable<IAttachmentFilter> _attachmentFilters;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentController"/>
        /// class.
        /// </summary>
        /// <param name="logger">An injected instance of the logger.</param>
        /// <param name="attachmentService">
        /// An injected instance of the attachment service.
        /// </param>
        /// <param name="attachmentFilters">
        /// An injected collection of attachment filters.
        /// </param>
        public AttachmentController(
            ILogger<AttachmentController> logger,
            IAttachmentService attachmentService,
            IEnumerable<IAttachmentFilter> attachmentFilters)
        {
            this._logger = logger;
            this._attachmentService = attachmentService;
            this._attachmentFilters = attachmentFilters;
        }

        /// <summary>
        /// Retrieves an attachment url from the attachment store.
        /// </summary>
        /// <param name="attachmentId">The attachment id to look up.</param>
        /// <returns>A URL if OK, an ErrorResponse if not.</returns>
        [HttpGet("{attachmentId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(object), 400)]
        public async Task<IActionResult> GetAttachmentUrl(string attachmentId)
        {
            // TODO: refactor AuthorizedUser out into dependency:
            var subject = this.User.FindFirstValue("sub") ??
                this.User.FindFirstValue(
                    "http://schemas.xmlsoap.org/ws/2005/05/" +
                    "identity/claims/nameidentifier");

            if (attachmentIdFilter.IsMatch(attachmentId))
            {
                this._logger.LogInformation(
                    $"Attachment {attachmentId} requested by {subject}");

                // TODO: Add authorization checks?
                return this.Ok(await this._attachmentService
                    .GetAttachmentUrlAsync(attachmentId));
            }
            else
            {
                this._logger.LogWarning(
                    $"Attachment id requested by {subject} not in valid " +
                    "format.");

                // TODO: Refactor out into dependency:
                return this.BadRequest(new
                {
                    code = "ERR_BAD_ATTACHMENT_ID",
                    message = "Error: Attachment ID is invalid.",
                    details = new Dictionary<string, string>
                    {
                        {
                            "attachmentId",
                            "does not match regex /^[a-fA-F0-9]+$/"
                        },
                    },
                });
            }
        }

        /// <summary>
        /// Uploads an attachment to the attachment store.
        /// </summary>
        /// <param name="attachmentId">The attachment id for the file.</param>
        /// <param name="file">The file to upload.</param>
        /// <returns>A URL of the file.</returns>
        [HttpPut("{attachmentId}")]
        public async Task<IActionResult> PutAttachment(
            string attachmentId,
            IFormFile file)
        {
            // TODO: refactor AuthorizedUser out into dependency:
            var subject = this.User.FindFirstValue("sub") ??
                this.User.FindFirstValue(
                    "http://schemas.xmlsoap.org/ws/2005/05/" +
                    "identity/claims/nameidentifier");

            if (file.Length > long.Parse(Environment
                .GetEnvironmentVariable("HOWLER_FILE_LIMIT") ?? "104857600"))
            {
                // TODO: Refactor out into dependency:
                return this.BadRequest(new
                {
                    code = "ERR_FILE_TOO_LARGE",
                    message = "Error: File too large",
                    details = new Dictionary<string, string>
                    {
                        {
                            "file",
                            "file too large"
                        },
                    },
                });
            }

            if (attachmentIdFilter.IsMatch(attachmentId))
            {
                using (var sha = SHA256.Create())
                {
                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);

                        var hash = await sha.ComputeHashAsync(ms);
                        var computedHash = string.Join(
                            string.Empty,
                            hash.Select(x => string.Format("{0:x2}", x)));

                        if (computedHash.Equals(attachmentId))
                        {
                            return await this.UploadAttachment(
                                subject,
                                computedHash,
                                ms);
                        }
                        else
                        {
                            this._logger.LogWarning(
                                $"Attachment uploaded by {subject} had " +
                                $"mismatching hash: {attachmentId} <> " +
                                $"{computedHash}");

                            // TODO: Refactor out into dependency:
                            return this.BadRequest(new
                            {
                                code = "ERR_BAD_ATTACHMENT_ID",
                                message = "Error: Attachment ID is invalid.",
                                details = new Dictionary<string, string>
                                {
                                    {
                                        "attachmentId",
                                        "does not match hash"
                                    },
                                },
                            });
                        }
                    }
                }
            }
            else
            {
                this._logger.LogWarning(
                    $"Attachment id uploaded by {subject} not in valid " +
                    "format.");

                // TODO: Refactor out into dependency:
                return this.BadRequest(new
                {
                    code = "ERR_BAD_ATTACHMENT_ID",
                    message = "Error: Attachment ID is invalid.",
                    details = new Dictionary<string, string>
                    {
                        {
                            "attachmentId",
                            "does not match regex /^[a-fA-F0-9]+$/"
                        },
                    },
                });
            }
        }

        private async Task<IActionResult> UploadAttachment(
            string subject,
            string computedHash,
            MemoryStream ms)
        {
            ms.Seek(0, SeekOrigin.Begin);
            await this._attachmentService.StageAttachmentAsync(
                computedHash,
                ms);

            var pass = this._attachmentFilters.AsParallel()
                .All(filter =>
                {
                    using (var newMs = new MemoryStream())
                    {
                        ms.CopyTo(newMs);

                        // TODO: gross, fix.
                        return filter.EvaluateAttachment(
                            computedHash,
                            subject,
                            newMs).Result;
                    }
                });

            if (!pass)
            {
                this._logger.LogWarning(
                    $"Attachment uploaded by {subject} did " +
                    $"not pass the content filter.");

                // TODO: Refactor out into dependency:
                return this.BadRequest(new
                {
                    code = "ERR_BAD_FILE",
                    message = "Error: File did not pass " +
                        "content filter.",
                    details = new Dictionary<string, string>
                    {
                        {
                            "file",
                            "file did not pass filter"
                        },
                    },
                });
            }
            else
            {
                return this.Ok(await this._attachmentService
                    .CommitAttachmentAsync(computedHash));
            }
        }
    }
}