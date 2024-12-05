using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteGymCommandHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork, ISubscriptionsRepository subscriptionRepository)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if (gym == null)
        {
            return Error.NotFound(description: "such gym was not found");
        }

        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(request.SubscriptionId);

        if(subscription is null)
        {
            return Error.NotFound(description: "requested subscription was not found");
        }

        if (!subscription.HasGym(gym.Id))
        {
            return Error.Unexpected(description: "gym not found for this subscription");
        }

        subscription.RemoveGym(gym.Id);

        await _subscriptionRepository.UpdateAsync(subscription);
        await _gymsRepository.RemoveGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
