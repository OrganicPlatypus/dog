using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using domesticOrganizationGuru.Validation;
using DomesticOrganizationGuru.Api.Tests.TestData;
using FluentValidation.TestHelper;
using System;
using Xunit;
using static FluentValidation.TestHelper.ValidationTestExtension;

namespace DomesticOrganizationGuru.Api.Tests
{
    public class UpdateNoteRequestDtoValidatorTests
    {
        private UpdateNoteRequestDtoValidator _notesSessionDtoValidator;
        public UpdateNoteRequestDtoValidatorTests()
        {
            _notesSessionDtoValidator = new();
        }

        [Theory]
        [ClassData(typeof(UpdateNoteRequestDtoValidatorTestsSuccessData))]
        public void UpdateNoteRequestDtoValidatorTests_Success_Tests(UpdateNoteRequestDto updateNotesSessionDto)
        {
            // Arrange

            // Act
            var result = _notesSessionDtoValidator.TestValidate(updateNotesSessionDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ExpirationMinutesRange);
            result.ShouldNotHaveValidationErrorFor(x => x.NotesPack);
            result.ShouldNotHaveValidationErrorFor(x => x.ConnectionId);
            result.ShouldNotHaveValidationErrorFor(x => x.NoteName);
            _notesSessionDtoValidator.ShouldHaveChildValidator(x => x.NotesPack, typeof(NoteDtoValidator));
        }

        [Theory]
        [ClassData(typeof(UpdateNoteRequestDtoValidatorTestsFailuresData))]
        public void UpdateNoteRequestDtoValidatorTests_Failures_Tests(UpdateNoteRequestDto updateNotesSessionDto)
        {
            // Arrange
            NoteDtoValidator childVlidation = new();

            // Act
            var result = _notesSessionDtoValidator.TestValidate(updateNotesSessionDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ExpirationMinutesRange);
            result.ShouldHaveValidationErrorFor(x => x.ConnectionId);
            result.ShouldHaveValidationErrorFor(x => x.NoteName);
            _notesSessionDtoValidator.ShouldHaveChildValidator(x => x.NotesPack, typeof(NoteDtoValidator));
        }
    }
}
