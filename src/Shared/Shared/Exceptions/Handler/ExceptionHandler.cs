﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Exceptions.Handler;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException =>
           (
             exception.Message,
             exception.GetType().Name,
             context.Response.StatusCode = StatusCodes.Status500InternalServerError
           ),

            BadRequestException =>
            (
             exception.Message,
             exception.GetType().Name,
             context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),

            NotFoundException =>
            (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status404NotFound
            ),

            ValidationException => 
            (
            exception.Message,
            exception.GetType().Name,
            context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),

            _ =>
            (
             exception.Message,
             exception.GetType().Name,
             context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Detail = details.Detail,
            Title = details.Title,
            Status = details.StatusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("TraceId", context.TraceIdentifier);

        if (exception is ValidationException validationException) problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        
        await context.Response.WriteAsJsonAsync( problemDetails,cancellationToken);
        return true;
    }
}
