using System;

using SHS.Services;
using SHS.Services.Exceptions;
using SHS.Models;


namespace SHS.Controllers.Exceptions
{
    public interface IApiExceptionHandler
    {
        IResponse BuildErrorResponse(IException exception);

        IResponse BuildErrorResponse(Exception exception);
    }

    public class ShsApiExceptionHandler : IApiExceptionHandler
    {
        public ShsApiExceptionHandler()
        {
        }

        public IResponse BuildErrorResponse(IException exception)
        {
            return new ShsResponse{
                ResponseCode = exception.Code,
                Message = exception.Message,
                Results = exception.Results
            };
        }

        public IResponse BuildErrorResponse(Exception exception)
        {
            return new ShsResponse{
                ResponseCode = ApiResponseCode.UnexpectedError,
                Message = "系統錯誤"
            };
        }
    }
}