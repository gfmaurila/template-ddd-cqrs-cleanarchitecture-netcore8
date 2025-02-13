using CSharpFunctionalExtensions;
using MediatR;
using Template.Common.Domain.Errors;

namespace Template.Application.Abstractions.Interface;

public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse, IDomainError>>
        where TRequest : ICommand<TResponse>
        where TResponse : notnull
{ }

public interface ICommandHandler<TRequest> : IRequestHandler<TRequest, Result<Unit>>
    where TRequest : ICommand
{ }