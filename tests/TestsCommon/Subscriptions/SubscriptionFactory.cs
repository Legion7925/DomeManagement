using GymManagement.Domain.Subscriptions;
using TestsCommon.Subscriptions.TestConstants;

namespace TestsCommon.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    {
        return new Subscription(subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
           adminId ?? Constants.Admin.Id,
           id ?? Constants.Subscription.Id);
    }
}
