using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Query.GetGym;

public record GetGymQuery(Guid SubscriptionId , Guid GymId) : IRequest<ErrorOr<Gym>>;
