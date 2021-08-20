using System.Collections.Generic;
using FluentValidation.Results;

using SHS.Models;


namespace SHS.Services.Exceptions
{
    /*  該物件已經存在, 導致後續操作會有問題
        For example, cannot create if the object is exist.
    */
    public class AgentExistsError : ShsException
    {
        public AgentExistsError(string message)
            : base(message, "F00"){}
    }

    /*  該物件不存在, 導致後續操作會有問題
    */
    public class AgentNotFoundError : ShsException
    {
        public AgentNotFoundError(string message)
            : base(message, "F01"){}
    }

    /*  Dto物件驗證失敗
    */
    public class ValidationError : ShsException
    {
        private List<ValidationFailure> _errors;

        public ValidationError(string message, List<ValidationFailure> errors)
            : base(message, "F02")
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
                        Name = error.PropertyName,
                        Code = error.ErrorCode,
                        Message = error.ErrorMessage
                    });
                }
                return results;
            }
        }
    }
}