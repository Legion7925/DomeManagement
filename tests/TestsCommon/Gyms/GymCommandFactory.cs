using GymManagement.Application.Gyms.Command.CreateGym;
using TestsCommon.Subscriptions.TestConstants;

namespace TestsCommon.Gyms;

public static class GymCommandFactory
{
    public static CreateGymCommand CreateGymCommand(string name =     Constants.Gym.Name,
        Guid? subscriptionId = null)
    {
        return new CreateGymCommand(name, subscriptionId ?? Constants.Subscription.Id);
    }
}
