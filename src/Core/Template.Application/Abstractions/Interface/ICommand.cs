using CSharpFunctionalExtensions;
using MediatR;
using Template.Common.Domain.Errors;

namespace Template.Application.Abstractions.Interface;

public interface ICommand<TResponse> : IRequestBase, IRequest<Result<TResponse, IDomainError>>
        where TResponse : notnull
{ }

public interface ICommand : IRequestBase, IRequest<Result<Unit>> { }
