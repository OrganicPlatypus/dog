using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using FluentValidation;
using System;

namespace domesticOrganizationGuru.Validation
{
    public class CreateNotesPackDtoValidator : AbstractValidator<CreateNoteDto>
    {
        public CreateNotesPackDtoValidator()
        {
            RuleFor(x => x.ExpirationMinutesRange)
                .GreaterThan(Convert.ToInt32(ValidationSettings.ExpiriationTimeMinimalValue))
                    .WithMessage(ValidationMessages.LifetimeInformation)
                .NotEmpty()
                    .WithMessage(ValidationMessages.LifetimeInformation)
                .NotNull()
                    .WithMessage(ValidationMessages.LifetimeInformation);

            RuleFor(x => x.NoteName)
                .MaximumLength(Convert.ToInt32(ValidationSettings.NoteNameLenth))
                    .WithMessage(ValidationMessages.ProvideNameInformation)
                .NotEmpty()
                    .WithMessage(ValidationMessages.ProvideNameInformation)
                .NotNull()
                    .WithMessage(ValidationMessages.ProvideNameInformation);
        }
    }
}
