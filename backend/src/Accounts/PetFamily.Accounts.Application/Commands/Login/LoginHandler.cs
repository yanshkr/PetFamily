using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Accounts.Application.Commands.Login;

public class LoginHandler : ICommandHandler<string, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly IValidator<LoginCommand> _validator;
    private readonly ILogger<LoginHandler> _logger;
    
    public LoginHandler(
        UserManager<User> userManager,
        ITokenProvider tokenProvider,
        IValidator<LoginCommand> validator,
        ILogger<LoginHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _validator = validator;
        _logger = logger;
    }
    public async Task<Result<string, ErrorList>> HandleAsync(
        LoginCommand command, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
            return Errors.General.NotFound(command.Email).ToErrorList();

        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (!passwordConfirmed)
            return Errors.General.UnExpected("Invalid password.").ToErrorList();
        
        return _tokenProvider.GenerateAccessToken(user);
    }
}