using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Response;
using PetFamily.Domain.Shared;

namespace PetFamily.Api.Extensions;
public static class ResponseExtensions
{
    public static ActionResult ToResponse<T>(this Result<T, ErrorList> result) =>
        new ObjectResult(result.IsFailure
            ? Envelope.Error(result.Error)
            : Envelope.Ok(result.Value))
        {
            StatusCode = result.IsFailure
                ? result.Error.FirstOrDefault()?.Type switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Failure => StatusCodes.Status500InternalServerError,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                }
                : StatusCodes.Status200OK
        };

}