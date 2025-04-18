﻿using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Core.Abstraction;
public interface ICommandHandler<TResponse, in TCommand> where TCommand : ICommand
{
    public Task<Result<TResponse, ErrorList>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task<UnitResult<ErrorList>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}