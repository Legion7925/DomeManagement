using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Command.CreateSubscription;

public record CreateSubscriptionCommand(SubscriptionType SubsctionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;