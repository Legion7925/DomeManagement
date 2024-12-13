using GymManagement.Domain.Admins.Events;
using GymManagement.Domain.Common;
using GymManagement.Domain.Subscriptions;
using Throw;

namespace GymManagement.Domain.Admins;

public class Admin : Entity
{

    public Guid UserId { get; }

    public Guid? SubscriptionId { get;private set; }

    public Admin(Guid userId , Guid? id = null , Guid? subscriptionId = null)
        :base(id ?? Guid.NewGuid())
    {
        UserId = userId;
        SubscriptionId = subscriptionId;
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

        _domainEvents.Add(new SubscriptionDeletedEvent(subId));
    }

    private Admin()
    {
    }
}
