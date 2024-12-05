using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Command.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subRepository, IUnitOfWork unitOfWork)
    {
        _subRepository = subRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new Subscription(request.SubsctionType, request.AdminId);

        await _subRepository.AddSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }
}
