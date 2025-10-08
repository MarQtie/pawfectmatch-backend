using FluentValidation;
using PawfectMatch.Backend.DTOs;

public class CreateLogValidator : AbstractValidator<CreateLogDto>
{
    public CreateLogValidator()
    {
        RuleFor(x => x.Action)
            .NotEmpty().WithMessage("Action is required.")
            .MaximumLength(200);

        RuleFor(x => x.Details)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrEmpty(x.Details));

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
