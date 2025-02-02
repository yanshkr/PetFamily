using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.Features.Files.DeleteFile;
using PetFamily.Application.Features.Files.GetFileUri;
using PetFamily.Application.Features.Files.UploadFile;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Application.Features.Volunteers.DeleteVolunteer;
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
        services.AddScoped<DeleteVolunteerHandler>();

        services.AddScoped<UploadFileHandler>();
        services.AddScoped<DeleteFileHandler>();
        services.AddScoped<GetFileUriHandler>();

        return services;
    }
}