using Microsoft.AspNetCore.Http;
using TaskoPhobia.Core.Exceptions;
using TaskoPhobia.Shared.Abstractions.Exceptions;
using TaskoPhobia.Shared.Abstractions.Exceptions.Errors;

namespace TaskoPhobia.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            await HandleExceptionAsync(e, context);
        }
    }

    private async Task HandleExceptionAsync(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException =>  (StatusCodes.Status400BadRequest, 
                new Error(exception.GetType().Name.Replace("Exception", string.Empty), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "Something went wrong")),
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
    
}