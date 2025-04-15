using FluentValidation.Results;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Core.Extensions;
public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        return validationResult.Errors
            .Select(e => Error.Deserialize(e.ErrorMessage))
            .ToList();
    }
}