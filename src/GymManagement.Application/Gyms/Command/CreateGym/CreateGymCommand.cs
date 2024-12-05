using ErrorOr;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Command.CreateGym;

public record CreateGymCommand(string name, Guid subscriptionId) : IRequest<ErrorOr<Gym>>;
