using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstraction;

namespace PetFamily.Accounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
    {
        services
            .AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly)
            .AddCommands();

        return services;
    }
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}