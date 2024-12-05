using GymManagement.Domain.Gyms;

namespace GymManagement.Application.Common.Interfaces;

public interface IGymsRepository
{
    Task AddGymAsync(Gym gym);
    Task<Gym?> GetByIdAsync(Guid gymId);
    Task<bool> ExistsAsync(Guid gymId);
    Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId);
    Task UpdateGymAsync(Gym gym);
    Task RemoveGymAsync(Gym gym);
    Task RemoveRangeAsync(List<Gym> gyms);
}
