using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Query.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly IGymsRepository _gymRepository;
    private readonly ISubscriptionsRepository _subscriptionRepository;

    public ListGymsQueryHandler(IGymsRepository gymRepository, ISubscriptionsRepository subscriptionRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery request, CancellationToken cancellationToken)
    {
        if (!await _subscriptionRepository.ExistsAsync(request.SubscriptionId))
        {
            return Error.NotFound(description: "subscription was not found!");
        }

        return await _gymRepository.ListBySubscriptionIdAsync(request.SubscriptionId);
    }
}
