using System;

using SHS.Services;
using SHS.Services.Exceptions;
using SHS.Models;


namespace SHS.Controllers.Exceptions
{
    public interface IApiExceptionHandler
    {
        IResponse BuildErrorResponse(AbcException exception);
    }

    public class ShsApiExceptionHandler : IApiExceptionHandler
    {
        public ShsApiExceptionHandler()
        {
        }

        public IResponse BuildErrorResponse(AbcException exception)
        {
            return new ShsResponse{
                ResponseCode = exception.Code,
                Message = exception.Message,
                Results = exception.Results
            };
        }
    }
}