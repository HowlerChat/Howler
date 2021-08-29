// <copyright file="AttachmentUrl.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Addressing
{
    using System;
    using Howler.Services.Models;
    using Howler.Services.Models.V1.Errors;

    /// <summary>
    /// Describes a url to locate attachments. Supports either
    /// application-native addressing or http-based addressing.
    /// </summary>
    public class AttachmentUrl : Uri
    {
        private AttachmentUrl(string url)
            : base(url)
        {
        }

        /// <summary>
        /// Parses a url, produces either a vanity url, or a validation error.
        /// </summary>
        /// <param name="url">The url to parse.</param>
        /// <returns>
        /// Either a successfully parsed attachment url or an error.
        /// </returns>
        public static Either<ErrorResponse, AttachmentUrl> Parse(string url)
        {
            try
            {
                var attachmentUrl = new AttachmentUrl(url);
                switch (attachmentUrl.Scheme)
                {
                    case "https":
                        if (
                            attachmentUrl.Host != "cdn.howler.chat" ||
                            !attachmentUrl.IsDefaultPort)
                        {
                            return new Models.Either<
                                ErrorResponse,
                                AttachmentUrl>(
                                new ValidationErrorResponse(
                                    "url",
                                    "INVALID_URL"));
                        }

                        break;
                    case "howler":
                        if (attachmentUrl.Host != "howler.eth")
                        {
                            return new Models.Either<
                                ErrorResponse,
                                AttachmentUrl>(
                                new ValidationErrorResponse(
                                    "url",
                                    "INVALID_URL"));
                        }

                        break;
                    default:
                        return new Models.Either<ErrorResponse, AttachmentUrl>(
                        new ValidationErrorResponse("url", "INVALID_URL"));
                }

                return new Models.Either<ErrorResponse, AttachmentUrl>(
                    attachmentUrl);
            }
            catch (UriFormatException)
            {
                return new Models.Either<ErrorResponse, AttachmentUrl>(
                    new ValidationErrorResponse("url", "INVALID_URL"));
            }
        }
    }
}
