using PetFamily.Api.Validation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace PetFamily.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(configuration =>
        {
            configuration.OverrideDefaultResultFactoryWith<CustomValidationFactory>();
        });
        return services;
    }
}
