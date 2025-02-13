using CSharpFunctionalExtensions;
using MediatR;
using Template.Common.Domain.Errors;

namespace Template.Application.Abstractions.Interface;

public interface IRequestBase { }
public interface IQuery<TResponse> : IRequestBase, IRequest<Result<TResponse, IDomainError>>
    where TResponse : notnull
{ }

public interface IQuery : IRequestBase, IRequest<Result>
{ }