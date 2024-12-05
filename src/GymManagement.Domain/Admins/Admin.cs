using GymManagement.Domain.Subscriptions;
using Throw;

namespace GymManagement.Domain.Admins;

public class Admin
{
    public Guid Id { get; private set; }

    public Guid UserId { get; }

    public Guid? SubscriptionId { get;private set; }

    public Admin(Guid userId , Guid? id = null , Guid? subscriptionId = null)
    {
        Id = id ?? Guid.NewGuid();
        UserId = userId;
        SubscriptionId = subscriptionId;
    }

    private Admin()
    {
    }

    public void SetSubscription(Subscription subscription)
    {
        SubscriptionId.HasValue.Throw().IfTrue();

        SubscriptionId = subscription.Id;
    }

    public void DeleteSubscription(Guid subId)
    {
        SubscriptionId.ThrowIfNull().IfNotEquals(subId);

        SubscriptionId = null;
    }
}
