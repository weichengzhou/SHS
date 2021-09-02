using System;

using SHS.Services;



namespace SHS.Services.Exceptions
{

    [Serializable]
    public class ShsException : Exception
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
}