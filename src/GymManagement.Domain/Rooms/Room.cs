namespace GymManagement.Domain.Rooms;

public class Room
{
    public Guid Id { get; }

    public string Name { get; } = null!;

    public Guid GymId { get; set; }

    public int MaxDailySessions { get; set; }

    public Room(string name , Guid gymId , int maxDailySesssions , Guid? id = null)
    {
        Name = name;
        GymId = gymId;
        MaxDailySessions = maxDailySesssions;
        Id = id ?? Guid.NewGuid();
    }

    private Room() { }
}
