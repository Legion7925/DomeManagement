using FluentValidation;

namespace GymManagement.Application.Gyms.Command.CreateGym;

public class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
{
    public CreateGymCommandValidator()
    {
        RuleFor(x=> x.name).MinimumLength(3).MaximumLength(100);
    }
}
