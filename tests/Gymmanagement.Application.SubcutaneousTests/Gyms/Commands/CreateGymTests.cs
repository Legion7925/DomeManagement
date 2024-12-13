using ErrorOr;
using FluentAssertions;
using GymManagement.Application.SubcutaneousTests.Common;
using GymManagement.Domain.Subscriptions;
using MediatR;
using TestsCommon.Gyms;
using TestsCommon.Subscriptions;

namespace Gymmanagement.Application.SubcutaneousTests.Gyms.Commands;

[Collection(MediatorFactoryCollection.CollectionName)]
public class CreateGymTests(MediatorFactory mediatorFactory)
{
    private readonly IMediator _mediator = mediatorFactory.CreateMediator();

    [Fact]
    public async void CreateGym_WhenValidCommand_ShouldCreateGym()
    {
        //Arrange
        //create a subscription
        var subscription = await CreateSubscription();

        //createa a valid CreateGymCommand
        var createGymCommand = GymCommandFactory.CreateGymCommand(subscriptionId: subscription.Id);

        //Act 
        var createGymResult = await _mediator.Send(createGymCommand);

        //Assert
        createGymResult.IsError.Should().BeFalse();
        createGymResult.Value.SubscriptionId.Should().Be(subscription.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(200)]
    public async Task CreateGym_WhenCommandContainsInvalidData_ShouldReturnsValidationError(int gymNameLength)
    {
        string gymName = new('a', gymNameLength);

        var createGymCommand = GymCommandFactory.CreateGymCommand(gymName);

        var result = await _mediator.Send(createGymCommand);

        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("name");
    }

    private async Task<Subscription> CreateSubscription()
    {
        var createSubscriptionCommand = SubscriptionCommandFactory.CreateSubscriptionCommand();

        var result = await _mediator.Send(createSubscriptionCommand);

        result.IsError.Should().BeFalse();
        return result.Value;
    }
}
