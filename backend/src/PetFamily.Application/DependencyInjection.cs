using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Species.AddBreed;
using PetFamily.Application.Features.Species.Create;
using PetFamily.Application.Features.Species.Delete;
using PetFamily.Application.Features.Volunteers.AddPet;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.DeletePhotosAtPet;
using PetFamily.Application.Features.Volunteers.DeleteVolunteer;
using PetFamily.Application.Features.Volunteers.UpdatePetPosition;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerMainInfo;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
using PetFamily.Application.Features.Volunteers.UploadPhotosToPet;

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
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<UploadPetPhotosHandler>();
        services.AddScoped<DeletePetPhotosHandler>();
        services.AddScoped<UpdatePetPositionHandler>();

        services.AddScoped<CreateSpecieHandler>();
        services.AddScoped<AddBreedHandler>();
        services.AddScoped<DeleteSpecieHandler>();

        return services;
    }
}