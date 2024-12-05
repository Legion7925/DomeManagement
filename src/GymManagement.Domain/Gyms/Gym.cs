using ErrorOr;
using GymManagement.Domain.Rooms;
using Throw;

namespace GymManagement.Domain.Gyms;

public class Gym
{
    public Guid Id { get; }

    public string Name { get; init; } = null!;

    public Guid SubscriptionId { get; init; }

    private readonly int _maxRooms;

    private readonly List<Guid> _trainerIds = new ();
    private readonly List<Guid> _roomIds = new ();

    public Gym(string name , int maxRooms , Guid subId , Guid? id = null)
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subId;
        Id = id ?? Guid.NewGuid();
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        _roomIds.Throw().IfContains(room.Id);

        if(_roomIds.Count >= _maxRooms)
        {
            return GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows;
        }

        _roomIds.Add(room.Id);

        return Result.Success;
    }

    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        if(_trainerIds.Contains(trainerId))
        {
            return Error.Conflict(description: "trainer already exists at this gym");
        }

        _trainerIds.Add(trainerId);

        return Result.Success;
    }

    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    public void RemoveRoom(Guid roomId)
    {
        _roomIds.Remove(roomId);
    }

    private Gym()
    {
    }
}
