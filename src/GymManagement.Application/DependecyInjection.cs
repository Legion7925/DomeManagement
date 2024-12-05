using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependecyInjection));
        });

        return services;
    }
}
