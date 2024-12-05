using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Command.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoomCommandHandler(IGymsRepository gymsRepository, ISubscriptionsRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _subscriptionRepository = subscriptionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if (gym is null)
        {
            return Error.NotFound(description: "gym was not found !");
        }

        var subscription = await _subscriptionRepository.GetSubscriptionByIdAsync(gym.SubscriptionId);

        if (subscription is null) { return Error.NotFound(description: "not found ffs"); }

        var room = new Room(request.RoomName, request.GymId, subscription.GetMaxDailySessions());

        var addRoomResult = gym.AddRoom(room);

        if (addRoomResult.IsError)
        {
            return addRoomResult.Errors;
        }

        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return room;
    }
}
