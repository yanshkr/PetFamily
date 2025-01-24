using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;

namespace PetFamily.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        return services;
    }
}