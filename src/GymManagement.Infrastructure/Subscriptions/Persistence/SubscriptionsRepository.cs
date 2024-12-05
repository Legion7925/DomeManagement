using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistance;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly GymManagementDbContext _context;

    public SubscriptionsRepository(GymManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
       await _context.Subscriptions.AddAsync(subscription);
    }

    public async Task<bool> ExistsAsync(Guid subscriptionId)
    {
        return await _context.Subscriptions
            .AsNoTracking()
            .AnyAsync(subscription => subscription.Id == subscriptionId);    
    }

    public async Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId)
    {
        var subscription = await _context.Subscriptions.FindAsync(subscriptionId);

        return subscription;
    }

    public Task RemoveSubscriptionAsync(Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(Subscription subscription)
    {
        _context.Update(subscription);

        return Task.CompletedTask;
    }
}
