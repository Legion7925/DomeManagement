using GymManagement.Domain.Gyms;
using TestsCommon.Subscriptions.TestConstants;

namespace TestsCommon.Gyms;

public class GymFactory
{
    public static Gym CreateGym(
        string name = Constants.Gym.Name,
        int maxRooms = Constants.Subscription.MaxRoomsFreeTier,
        Guid? id = null)
    {
        return new Gym(
            name ,
            maxRooms ,
            subId: Constants.Subscription.Id,
            id: id ?? Constants.Gym.Id);
    }
}
