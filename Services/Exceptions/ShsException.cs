using System;

using SHS.Services;



namespace SHS.Services.Exceptions
{
    /// <summary>
    /// Customize exception used for SHS.
    /// </summary>
    [Serializable]
    public class ShsException : Exception
    {
        protected string _code;

        /// <summary>
        /// Constructor exception without params.
        /// </summary>
        public ShsException() : base()
        {
        }

        /// <summary>
        /// Construct exception with message and error code.
        /// </summary>
        public ShsException(string message, string code="") : base(message)
        {
            this._code = code;
        }

        /// <summary>
        /// Error code.
        /// </sumamry>
        public virtual string Code
        {
            get => this._code;
        }

        /// <summary>
        /// The results of exception.
        /// </summary>
        public virtual object Results
        {
            get => null;
        }
    }
}