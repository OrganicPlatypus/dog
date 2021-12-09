using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using FluentValidation;
using System;

namespace domesticOrganizationGuru.Validation
{
    public class CreateNotesPackDtoValidator : AbstractValidator<CreateNotesPackDto>
    {
        public CreateNotesPackDtoValidator()
        {
            RuleFor(x => x.ExpirationMinutesRange)
                .GreaterThan(Convert.ToInt32(ValidationSettings.ExpiriationTimeMinimalValue))
                .NotEmpty()
                .WithMessage(ValidationMessages.LifetimeInformation);

            RuleFor(x => x.NoteName)
                .MaximumLength(Convert.ToInt32(ValidationSettings.NoteNameLenth))
                .NotEmpty()
                .WithMessage(ValidationMessages.ProvideNameInformation);
        }
    }
}
