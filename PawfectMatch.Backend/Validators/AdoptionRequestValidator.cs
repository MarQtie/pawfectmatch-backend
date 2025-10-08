using FluentValidation;
using PawfectMatch.Backend.DTOs;

public class CreateAdoptionRequestValidator : AbstractValidator<CreateAdoptionRequestDto>
{
    public CreateAdoptionRequestValidator()
    {
        RuleFor(x => x.PetId)
            .NotEmpty().WithMessage("PetId is required.");

        RuleFor(x => x.AdopterId)
            .NotEmpty().WithMessage("AdopterId is required.");
    }
}

public class UpdateAdoptionRequestValidator : AbstractValidator<UpdateAdoptionRequestDto>
{
    public UpdateAdoptionRequestValidator()
    {
        RuleFor(x => x.Status)
            .Must(s => new[] { "Pending", "Approved", "Rejected" }.Contains(s))
            .When(x => !string.IsNullOrEmpty(x.Status))
            .WithMessage("Invalid status value.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}
