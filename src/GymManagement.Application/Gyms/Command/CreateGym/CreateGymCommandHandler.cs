using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Command.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymsRepository;

    private readonly ISubscriptionsRepository _subscriptionRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateGymCommandHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(request.subscriptionId);

        if (subscription == null)
        {
            return Error.NotFound(description: "subscription not found");
        }

        var gym = new Gym(request.name, subscription.GetMaxRooms(), subscription.Id);

        var addGymResult = subscription.AddGym(gym);

        if (addGymResult.IsError)
        {
            return addGymResult.Errors;
        }

        await _subscriptionRepository.UpdateAsync(subscription);
        await _gymsRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}
