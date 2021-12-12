using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using FluentValidation;
using System;

namespace domesticOrganizationGuru.Validation
{
    public class NoteDtoValidator : AbstractValidator<NoteDto>
    {
        public NoteDtoValidator()
        {
            RuleFor(x => x.NoteText)
                .MaximumLength(Convert.ToInt32(ValidationSettings.MaxNoteLength))
                    .WithMessage(
                        string.Format($"{ValidationMessages.NoteMaxLengthPrefix}{ValidationSettings.MaxNoteLength}{ValidationMessages.NoteMaxLengthSufix}")
                        )
                .NotEmpty()
                    .WithMessage(ValidationMessages.NoteEmptyInformation)
                .NotNull()
                    .WithMessage(ValidationMessages.NoteEmptyInformation);

        }
    }
}
