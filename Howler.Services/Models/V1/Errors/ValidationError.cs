namespace Howler.Services.Models.V1.Errors
{
    using System.Collections.Generic;

    public class ValidationError
    {
        public ValidationError(string propertyName, string propertyErrorCode, Dictionary<string, string>? additionalInfo = null) =>
            (this.PropertyName, this.PropertyErrorCode, this.AdditionalInfo) = (propertyName, propertyErrorCode, additionalInfo);
            
        /// <summary>
        /// The name of the property with validation errors
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// The error code associated with the validation error
        /// </summary>
        public string PropertyErrorCode { get; set; }

        /// <summary>
        /// Any additional information associated with the given error code
        /// </summary>
        public Dictionary<string, string>? AdditionalInfo { get; set; }
    }
}