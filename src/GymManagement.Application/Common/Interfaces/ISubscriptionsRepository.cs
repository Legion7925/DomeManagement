using ErrorOr;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<bool> ExistsAsync(Guid subscriptionId);
    Task<Subscription?> GetSubscriptionByIdAsync(Guid subscriptionId);
    Task RemoveSubscriptionAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
}
