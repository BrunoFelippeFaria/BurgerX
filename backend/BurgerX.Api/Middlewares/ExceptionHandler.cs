
using System.Net;

using BurgerX.Domain.Exceptions;

namespace BurgerX.Api.Middlewares;

public class ExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (DomainException ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            if (ex is NotFoundException)
                statusCode = HttpStatusCode.NotFound;

            else if (ex is ConflictException)
                statusCode = HttpStatusCode.Conflict;

            await HandleError(context, statusCode, ex.Message, ex.Code);
        }
    }
    
    private static async Task HandleError(HttpContext context, HttpStatusCode statusCode, string error, string code)
    {
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            error,
            code
        });        
    }
}