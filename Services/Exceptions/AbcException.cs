using System;

using SHS.Services;



namespace SHS.Services.Exceptions
{
    public abstract class AbcException : Exception
    {
        public AbcException() : base()
        {
        }

        public AbcException(string message) : base(message)
        {
        }

        public abstract string Code
        {
            get;
        }

        public abstract object Results
        {
            get;
        }
    }

    [Serializable]
    public class ShsException : AbcException
    {
        protected string _code;

        public ShsException() : base()
        {
        }

        public ShsException(string message, string code="") : base(message)
        {
            this._code = code;
        }

        public override string Code
        {
            get => this._code;
        }

        public override object Results
        {
            get => null;
        }
    }

    [Serializable]
    public class UnexpectedException : AbcException
    {
        public UnexpectedException(string message="系統錯誤") : base(message)
        {
        }

        public override string Code
        {
            get => ApiResponseCode.UnexpectedError;
        }

        public override object Results
        {
            get => null;
        }
    }
}