using GymManagement.Application.Subscriptions.Command.CreateSubscription;
using GymManagement.Application.Subscriptions.Command.DeleteSubscription;
using GymManagement.Application.Subscriptions.Query;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender _mediator;

    public SubscriptionsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString() , out var subscriptionType)
)
        {
            return Problem(statusCode: StatusCodes.Status400BadRequest , detail: "Invalid Subscription Type");
        }

        var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);

        var createSubResult = await _mediator.Send(command);

        return createSubResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(subscription.Id, request.SubscriptionType)),
            error => Problem());
    }

    [HttpGet]
    public async Task<IActionResult> GetSubscription(Guid subId)
    {
        var query = new GetSubscriptionQuery(subId);

        var getSubResult = await _mediator.Send(query);

        return getSubResult.MatchFirst(
            subscription => Ok(new SubscriptionResponse(
                   subscription.Id,
                   Enum.Parse<SubscriptionType>(subscription.SubscriptionType.Name)
                )),
            error => Problem()
            );
    }

    [HttpDelete("{subscriptionId:guid}")]
    public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
    {
        var command = new DeleteSubscriptionCommand(subscriptionId);

        var createSubscriptionResult = await _mediator.Send(command);

        return createSubscriptionResult.Match<IActionResult>(
            _ => NoContent(),
            _ => Problem());
    }

    private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
    {
        return subscriptionType.Name switch
        {
            nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
            nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
            nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
            _ => throw new InvalidOperationException(),
        };
    }
}
