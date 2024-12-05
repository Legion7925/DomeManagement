using ErrorOr;
using MediatR;

namespace GymManagement.Application.Subscriptions.Command.DeleteSubscription;

public record DeleteSubscriptionCommand(Guid SubscriptionId) : IRequest<ErrorOr<Deleted>>;
