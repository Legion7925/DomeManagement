using ErrorOr;

namespace GymManagement.Domain.Subscriptions;

public static class SubscriptionErrors
{
    public static readonly Error CannotHaveMoreGymsThanSubscriptionAllows = Error.Validation(
        "Room.CannotHaveMoreRoomsThanSubscriptionAllows",
        "A gym cannot have more rooms than the subscription allows");
}
