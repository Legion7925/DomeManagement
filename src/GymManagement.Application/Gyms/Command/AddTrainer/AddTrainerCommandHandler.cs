﻿using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Command.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTrainerCommandHandler(IGymsRepository gymsRepository, IUnitOfWork unitOfWork)
    {
        _gymsRepository = gymsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(request.GymId);

        if(gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        var addTrainerResult = gym.AddTrainer(request.TrainerId);

        if(addTrainerResult.IsError)
        {
            return addTrainerResult.Errors;
        }

        await _gymsRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}
