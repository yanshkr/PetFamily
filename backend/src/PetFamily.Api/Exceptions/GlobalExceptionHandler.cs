using Microsoft.AspNetCore.Diagnostics;
using PetFamily.Api.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
        )
    {
        _logger.LogError(exception, "An unhandled exception has occurred");

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(
            Envelope.Error([Errors.General.UnExpected(exception.Message)]),
            cancellationToken);

        return true;
    }
}
