using CSharpFunctionalExtensions;
using MediatR;
using Template.Common.Domain.Errors;

namespace Template.Application.Abstractions.Interface;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse, IDomainError>>
       where TRequest : IQuery<TResponse>
       where TResponse : notnull
{ }
