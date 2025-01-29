using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.UpdateVolunteer;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;

namespace PetFamily.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateVolunteerMainInfoHandler>();
        services.AddScoped<UpdateVolunteerSocialMediaHandler>();
        services.AddScoped<UpdateVolunteerPaymentInfoHandler>();

        return services;
    }
}