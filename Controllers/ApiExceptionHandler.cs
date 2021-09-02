using System;
using System.Net;

using SHS.Services;
using SHS.Services.Exceptions;
using SHS.Models;


namespace SHS.Controllers.Exceptions
{
    /// <summary>
    /// Use to handle api exception.
    /// </summary>
    public interface IApiExceptionHandler
    {
        /// <summary>
        /// Get exception, and handle it to IResponse.
        /// Ref. Models/Response.cs
        /// </summary>
        /// <param name="exception">The exception catched by system.</param>
        /// <returns>The api response contains error message.</returns>
        IResponse GetErrorResponse(Exception exception);

        /// <summary>
        /// Get exception, and return corresponding http code.
        /// </summary>
        int GetHttpCode(Exception exception);
    }

    /// <summary>
    /// Use to handle api exception in SHS system.
    /// </summary>
    public class ShsApiExceptionHandler : IApiExceptionHandler
    {
        public ShsApiExceptionHandler()
        {
        }

        /// <sumamry>
        /// Handle exception to ShsResponse.
        /// Ref. Models/Response.cs
        /// </summary>
        /// <param name="exception">The exception raised by system.</param>
        /// <returns>The response defined by system.</returns>
        public IResponse GetErrorResponse(Exception exception)
        {
            if(exception is ShsException)
            {
                ShsException shsException = (ShsException)exception;
                return new ShsResponse{
                    ResponseCode = shsException.Code,
                    Message = shsException.Message,
                    Results = shsException.Results
                };
            }
            else
            {
                return new ShsResponse{
                    ResponseCode = ApiResponseCode.UnexpectedError,
                    Message = "系統錯誤",
                    Results = null
                };
            }
        }

        /// <summary>
        /// Get http code according by exception.
        /// If exception is expected, return 400.
        /// If exception is not excpeted, return 500.
        /// </summary>
        /// <param name="exception">The exception raised by system.</param>
        /// <returns>The http code.</returns>
        public int GetHttpCode(Exception exception)
        {
            if(exception is ShsException)
            {
                return (int)HttpStatusCode.BadRequest;
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}