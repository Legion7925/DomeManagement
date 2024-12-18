using ErrorOr;
using GymManagement.Application.Common.Auhtorization;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using System.Reflection;

namespace GymManagement.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(ICurrentUserProvider _currentUserProvider)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizationAttributes = request.GetType()
            .GetCustomAttributes<AuthorizeAttribute>()
            .ToList();

        if(authorizationAttributes.Count == 0)
        {
            return next();
        }

        var requiredPermissions = authorizationAttributes.SelectMany(authorizationAttributes => authorizationAttributes.Permissions?.Split(',') ?? []).ToList();

        var currentUser = _currentUserProvider.GetCurrentUser();

        if(requiredPermissions.Except(currentUser.Permissions).Any())
        {
            return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");
        }

        var requiredRoles = authorizationAttributes.SelectMany(authorizationAttributes => authorizationAttributes.Roles?.Split(',') ?? []).ToList();

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            return (dynamic)Error.Unauthorized(description: "User is forbidden from taking this action");
        }

        return next();
    }
}
