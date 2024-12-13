using GymManagement.Application.Subscriptions.Command.CreateSubscription;
using GymManagement.Domain.Subscriptions;
using TestsCommon.Subscriptions.TestConstants;

namespace TestsCommon.Subscriptions;

public static class SubscriptionCommandFactory
{
    public static CreateSubscriptionCommand CreateSubscriptionCommand(SubscriptionType? subscriptionType = null,
        Guid? adminId = null)
    {
        return new CreateSubscriptionCommand(
            subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
            adminId ?? Constants.Admin.Id);
    }
}
