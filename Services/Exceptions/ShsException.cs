using System;

using SHS.Services;



namespace SHS.Services.Exceptions
{
    public interface IException
    {
        string Code { get; }

        string Message { get; }
        
        object Results { get; }
    }

    [Serializable]
    public class ShsException : Exception, IException
    {
        protected string _code;

        public ShsException() : base()
        {
        }

        public ShsException(string message, string code="") : base(message)
        {
            this._code = code;
        }

        public virtual string Code
        {
            get => this._code;
        }

        public virtual object Results
        {
            get => null;
        }
    }

    [Serializable]
    public class UnexpectedException : Exception, IException
    {
        public UnexpectedException() : base()
        {
        }
        
        public UnexpectedException(string message) : base(message)
        {
        }

        public string Code
        {
            get => ApiResponseCode.UnexpectedError;
        }

        public object Results
        {
            get => null;
        }
    }
}