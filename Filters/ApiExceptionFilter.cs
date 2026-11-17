using FitTrack.DTOs.Common;
using FitTrack.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FitTrack.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        int status;
        string message;

        if (exception is NotFoundException)
        {
            status = 404;
            message = exception.Message;
        }
        else if (exception is BusinessRuleException)
        {
            status = 400;
            message = exception.Message;
        }
        else if (exception is ConflictException)
        {
            status = 409;
            message = exception.Message;
        }
        else if (exception is ForbiddenException)
        {
            status = 403;
            message = exception.Message;
        }
        else if (exception is UnauthorizedException)
        {
            status = 401;
            message = exception.Message;
        }
        else
        {
            status = 500;
            message = "An unexpected error occurred.";
        }

        var body = new ErrorResponse
        {
            Status = status,
            Message = message
        };

        context.Result = new ObjectResult(body) { StatusCode = status };
        context.ExceptionHandled = true;
    }
}
