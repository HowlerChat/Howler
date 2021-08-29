// <copyright file="VanityUrl.cs" company="Howler Team">
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
    /// Describes a url to locate spaces via customizable phrase. Supports
    /// either application-native addressing or http-based addressing.
    /// </summary>
    public class VanityUrl : Uri
    {
        private VanityUrl(string url)
            : base(url)
        {
        }

        /// <summary>
        /// Parses a url, produces either a vanity url, or a validation error.
        /// </summary>
        /// <param name="url">The url to parse.</param>
        /// <returns>
        /// Either a successfully parsed vanity url or an error.
        /// </returns>
        public static Either<ErrorResponse, VanityUrl> Parse(string url)
        {
            try
            {
                var vanityUrl = new VanityUrl(url);
                switch (vanityUrl.Scheme)
                {
                    case "https":
                        if (
                            vanityUrl.Host != "howler.gg" ||
                            !vanityUrl.IsDefaultPort)
                        {
                            return new Models.Either<ErrorResponse, VanityUrl>(
                                new ValidationErrorResponse(
                                    "url",
                                    "INVALID_URL"));
                        }

                        break;
                    case "howler":
                        if (vanityUrl.Host != "howler.eth")
                        {
                            return new Models.Either<ErrorResponse, VanityUrl>(
                                new ValidationErrorResponse(
                                    "url",
                                    "INVALID_URL"));
                        }

                        break;
                    default:
                        return new Models.Either<ErrorResponse, VanityUrl>(
                        new ValidationErrorResponse("url", "INVALID_URL"));
                }

                return new Models.Either<ErrorResponse, VanityUrl>(vanityUrl);
            }
            catch (UriFormatException)
            {
                return new Models.Either<ErrorResponse, VanityUrl>(
                    new ValidationErrorResponse("url", "INVALID_URL"));
            }
        }
    }
}
