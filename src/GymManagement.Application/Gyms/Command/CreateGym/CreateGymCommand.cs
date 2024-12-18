using ErrorOr;
using GymManagement.Application.Common.Auhtorization;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Command.CreateGym;

[Authorize(Roles ="Admin")]
public record CreateGymCommand(string name, Guid subscriptionId) : IRequest<ErrorOr<Gym>>;
