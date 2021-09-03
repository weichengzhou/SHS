namespace SHS.Models
{
    /// <summary>
    /// The interface defined response.
    /// </summary>
    public interface IResponse
    {
    }    

    /// <summary>
    /// Define the response in SHS.
    /// </summary>
    public class ShsResponse : IResponse
    {
        /// <summary>
        /// Response code defined by system.
        /// Ref. Services/ApiResponseCode.cs
        /// </summary>
        public string ResponseCode { get; set; }

        /// <summary>
        /// The message of response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The results of message, can be null.
        /// </summary>
        public object Results { get; set; }
    }

    /// <summary>
    /// Validation error in field.
    /// </summary>
    public class ShsFieldError
    {
        /// <summary>
        /// Name of field which is validation error.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Error code of field.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Message about validation error.
        /// </summary>
        public string Message { get; set; }
    }
}