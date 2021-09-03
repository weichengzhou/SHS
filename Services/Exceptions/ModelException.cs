using System.Collections.Generic;
using FluentValidation.Results;

using SHS.Services;
using SHS.Models;


namespace SHS.Services.Exceptions
{
    /// <summary>
    /// The agent is exist, cause the operation error.
    /// For example, cannot create if the object is exist.
    /// </summary>
    public class AgentExistsError : ShsException
    {
        /// <summary>
        /// Constructor of AgentExistsError.
        /// </summary>
        /// <param name="message">The message of error.</param>
        public AgentExistsError(string message)
            : base(message, ApiResponseCode.AgentIsExist)
        {
        }
    }

    /// <summary>
    /// The agent is not exist, cause the operation error.
    /// </summary>
    public class AgentNotFoundError : ShsException
    {
        /// <summary>
        /// Constructor of AgentNotFoundError.
        /// </summary>
        /// <param name="message">The message of error.</param>
        public AgentNotFoundError(string message)
            : base(message, ApiResponseCode.AgentNotExist)
        {
        }
    }

    /// <summary>
    /// Object validation error, usually used to DTOs.
    /// </summary>
    public class ValidationError : ShsException
    {
        private List<ValidationFailure> _errors;

        /// <summary>
        /// Constructor of ValidationError.
        /// </summary>
        /// <param name="message">Message of error.</param>
        /// <param name="errors">List of validation error.</param>
        public ValidationError(string message, List<ValidationFailure> errors)
            : base(message, ApiResponseCode.ValidationError)
        {
            this._errors = errors;
        }

        /// <summary>
        /// Pack errors to result.
        /// </summary>
        public override object Results
        {
            get
            {
                List<ShsFieldError> results = new List<ShsFieldError>();
                foreach(ValidationFailure error in this._errors)
                {
                    results.Add(new ShsFieldError{
                        FieldName = error.PropertyName,
                        ErrorCode = error.ErrorCode,
                        Message = error.ErrorMessage
                    });
                }
                return results;
            }
        }
    }
}