using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Command.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subRepository;
    private readonly IAdminsRepository _adminsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subRepository, IUnitOfWork unitOfWork, IAdminsRepository adminsRepository)
    {
        _subRepository = subRepository;
        _unitOfWork = unitOfWork;
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var admin =await _adminsRepository.GetByIdAsync(request.AdminId);

        if (admin == null)
        {
            return Error.NotFound("admin with this admin id was not found");
        }

        var subscription = new Subscription(request.SubsctionType, request.AdminId);

        if(admin.SubscriptionId is not null)
        {
            return Error.Conflict("Admin already has an active subscription");
        }

        admin.SetSubscription(subscription);

        await _adminsRepository.UpdateAsync(admin);
        await _subRepository.AddSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        return subscription;
    }
}
