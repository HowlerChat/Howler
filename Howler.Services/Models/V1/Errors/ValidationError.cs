// <copyright file="ValidationError.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.Services.Models.V1.Errors
{
    using System.Collections.Generic;

    /// <summary>
    /// Captures a standardized validation error response.
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationError"/>
        /// class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property failing validation.
        /// </param>
        /// <param name="propertyErrorCode">
        /// The error code applying to the property.
        /// </param>
        /// <param name="additionalInfo">
        /// Any additional information to accompany the validation error.
        /// </param>
        public ValidationError(
            string propertyName,
            string propertyErrorCode,
            Dictionary<string, string>? additionalInfo = null)
        {
            this.PropertyName = propertyName;
            this.PropertyErrorCode = propertyErrorCode;
            this.AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Gets the name of the property with validation errors.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets the error code associated with the validation error.
        /// </summary>
        public string PropertyErrorCode { get; }

        /// <summary>
        /// Gets any additional information associated with the given error
        /// code.
        /// </summary>
        public Dictionary<string, string>? AdditionalInfo { get; }
    }
}