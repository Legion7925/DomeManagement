using ErrorOr;
using MediatR;

namespace GymManagement.Application.Rooms.Command.DeleteRoom;

public record DeleteRoomCommand(Guid GymId,Guid RoomId) : IRequest<ErrorOr<Deleted>>;
