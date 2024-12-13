using ErrorOr;
using FluentValidation;
using GymManagement.Application.Common.Behaviors;
using GymManagement.Application.Gyms.Command.CreateGym;
using GymManagement.Domain.Gyms;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependecyInjection));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>)); 
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependecyInjection));

        return services;
    }
}
