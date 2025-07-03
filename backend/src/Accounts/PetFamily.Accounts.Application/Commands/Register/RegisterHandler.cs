using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Accounts.Application.Commands.Register;
public class RegisterHandler : ICommandHandler<string, RegisterCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly ILogger<RegisterHandler> _logger;

    public RegisterHandler(
        UserManager<User> userManager, 
        IValidator<RegisterCommand> validator,
        ILogger<RegisterHandler> logger)
    {
        _userManager = userManager;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<string, ErrorList>> HandleAsync(
        RegisterCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var userByEmail = await _userManager.FindByEmailAsync(command.Email);
        if (userByEmail is not null)
            return Errors.General.UnExpected("Email already registered.").ToErrorList();
        
        var userByUsername = await _userManager.FindByNameAsync(command.Username);
        if (userByUsername is not null)
            return Errors.General.UnExpected("Username already registered.").ToErrorList();
        
        var user = new User
        {
            Email = command.Email,
            UserName = command.Username,
        };

        var result = await _userManager.CreateAsync(user, command.Password);
        return result.Succeeded ? 
            command.Email : 
            new ErrorList([..result.Errors.Select(e => Error.Failure(e.Code, e.Description))]);
    }
}