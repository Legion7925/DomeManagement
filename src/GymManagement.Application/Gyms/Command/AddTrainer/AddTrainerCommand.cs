using ErrorOr;
using MediatR;

namespace GymManagement.Application.Gyms.Command.AddTrainer;

public record AddTrainerCommand(Guid GymId , Guid SubscriptionId , Guid TrainerId) : IRequest<ErrorOr<Success>>;
