using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using FluentValidation;
using System;

namespace domesticOrganizationGuru.Validation
{
    public class NotesSessionDtoValidator : AbstractValidator<NotesSessionDto>
    {
        public NotesSessionDtoValidator()
        {
            RuleFor(x => x.ExpirationMinutesRange)
                .GreaterThan(Convert.ToInt32(ValidationSettings.ExpiriationTimeMinimalValue))
                    .WithMessage(ValidationMessages.LifetimeInformation)
                .NotEmpty()
                    .WithMessage(ValidationMessages.LifetimeInformation)
                .NotNull()
                    .WithMessage(ValidationMessages.LifetimeInformation);

            When(notesSession => notesSession.Notes.Length > 0, () =>
            {
                RuleForEach(x => x.Notes).SetValidator(new NoteDtoValidator());
            });
        }
    }
}
