using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Core.Validation;
public static class CustomValidators
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(Error.Serialize(result.Error));
        });
    }
}
