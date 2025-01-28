using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetFamily.Api.Response;
using PetFamily.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetFamily.Api.Validation;

public class CustomValidationFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails
        )
    {
        if (validationProblemDetails is null)
            throw new InvalidOperationException("ValidationProblemDetails is null");

        List<Error> errors = [];
        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            errors.AddRange(validationErrors.Select(Error.Deserialize));
        }

        return new ObjectResult(Envelope.Error(errors))
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}