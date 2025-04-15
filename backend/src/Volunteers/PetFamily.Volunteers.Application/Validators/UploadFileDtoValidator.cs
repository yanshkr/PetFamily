using FluentValidation;
using PetFamily.Volunteers.Contracts.Dtos.Pet;

namespace PetFamily.Volunteers.Application.Validators;
public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {

    }
}
