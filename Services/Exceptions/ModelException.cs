using System.Collections.Generic;
using FluentValidation.Results;

using SHS.Services;
using SHS.Models;


namespace SHS.Services.Exceptions
{
    /*  The agent is exist, cause the operation error.
        For example, cannot create if the object is exist.
    */
    public class AgentExistsError : ShsException
    {
        public AgentExistsError(string message)
            : base(message, ApiResponseCode.AgentIsExist)
        {
        }
    }

    /*  The agent is not exist, cause the operation error.
    */
    public class AgentNotFoundError : ShsException
    {
        public AgentNotFoundError(string message)
            : base(message, ApiResponseCode.AgentNotExist)
        {
        }
    }

    /*  Dto object validation error.
    */
    public class ValidationError : ShsException
    {
        private List<ValidationFailure> _errors;

        public ValidationError(string message, List<ValidationFailure> errors)
            : base(message, ApiResponseCode.ValidationError)
        {
            this._errors = errors;
        }

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