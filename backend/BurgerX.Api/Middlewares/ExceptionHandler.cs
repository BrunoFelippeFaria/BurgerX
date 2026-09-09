
using System.Net;

using BurgerX.Domain.Shared.Exceptions;

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

            else if (ex is UnauthorizedException)
                statusCode = HttpStatusCode.Unauthorized;

            await HandleError(context, statusCode, ex.Message, ex.Code);
        }

        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                code = "validation_error",
                errors = ex.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                })
            });

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