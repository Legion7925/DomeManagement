using GymManagement.Application.Common.Interfaces;
using GymManagement.Infrastructure.Admins.Persistance;
using GymManagement.Infrastructure.Common.Persistance;
using GymManagement.Infrastructure.Gyms.Persistance;
using GymManagement.Infrastructure.Subscriptions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagement.Infrastructure;

public static class DependecyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<GymManagementDbContext>(options =>
        {
            options.UseSqlite("Data Source = GymManagement.db");
        });

        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<GymManagementDbContext>());

        return services;
    }
}
