using ErrorOr;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Application.Rooms.Command.CreateRoom;

public record CreateRoomCommand(Guid GymId , string RoomName) : IRequest<ErrorOr<Room>>;

