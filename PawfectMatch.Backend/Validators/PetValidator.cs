using FluentValidation;
using PawfectMatch.Backend.DTOs;

public class CreatePetValidator : AbstractValidator<CreatePetDto>
{
    public CreatePetValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Pet name is required.")
            .MaximumLength(50);

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Species is required.");

        RuleFor(x => x.Age)
            .InclusiveBetween(0, 50).WithMessage("Age must be between 0 and 50.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("OwnerId is required.");
    }
}

public class UpdatePetValidator : AbstractValidator<UpdatePetDto>
{
    public UpdatePetValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Age)
            .InclusiveBetween(0, 50)
            .When(x => x.Age.HasValue);
    }
}
