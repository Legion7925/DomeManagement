﻿using ErrorOr;
using FluentAssertions;
using GymManagement.Domain.Subscriptions;
using TestsCommon.Gyms;
using TestsCommon.Subscriptions;

namespace GymManagement.Domain.UnitTests.Subscriptions;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var subscription = SubscriptionFactory.CreateSubscription();

        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid())).ToList();

        // Act
        var addGymResults = gyms.ConvertAll(subscription.AddGym);

        // Assert
        var allBuLastGymResults = addGymResults[..^1];
        allBuLastGymResults.Should().AllSatisfy(addGymResult => addGymResult.Value.Should().Be(Result.Success));

        var lastAddGymResult = addGymResults.Last();

        lastAddGymResult.IsError.Should().BeTrue();
        lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}
