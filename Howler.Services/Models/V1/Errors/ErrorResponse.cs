// <copyright file="ErrorResponse.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Errors
{
    /// <summary>
    /// Describes a general error response.
    /// </summary>
    public abstract class ErrorResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorResponse"/>
        /// class.
        /// </summary>
        /// <param name="errorCode">The error code, in CAPS_SNAKE_CASE.</param>
        public ErrorResponse(string errorCode) =>
            this.ErrorCode = errorCode;

        /// <summary>
        /// Gets the code corresponding to the error.
        /// </summary>
        public string ErrorCode { get; }
    }
}
