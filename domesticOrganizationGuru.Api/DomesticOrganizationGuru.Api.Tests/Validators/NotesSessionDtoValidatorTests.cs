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
    public class NotesSessionDtoValidatorTests
    {
        private NotesSessionDtoValidator _notesSessionDtoValidator;
        public NotesSessionDtoValidatorTests()
        {
            _notesSessionDtoValidator = new();
        }

        [Theory]
        [ClassData(typeof(NotesSessionDtoValidatorTestsSuccessData))]
        public void NotesSessionDtoValidatorTests_Success_Test(NotesSessionDto notesSessionDto)
        {
            // Arrange

            // Act
            var result = _notesSessionDtoValidator.TestValidate(notesSessionDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ExpirationMinutesRange);
            result.ShouldNotHaveValidationErrorFor(x => x.Notes);
        }

        [Theory]
        [ClassData(typeof(NotesSessionDtoValidatorTestsFailuresData))]
        public void NotesSessionDtoValidatorTests_Failures_Test(NotesSessionDto notesSessionDto, bool faultyExpiriation, bool faultyNotes, string errorMessage)
        {
            // Arrange
            NoteDtoValidator childVlidation = new();

            // Act
            var result = _notesSessionDtoValidator.TestValidate(notesSessionDto);

            // Assert
            if (faultyExpiriation)
            {
                result.ShouldHaveValidationErrorFor(x => x.ExpirationMinutesRange)
                        .WithErrorMessage(errorMessage);
            }
            if (faultyNotes)
            {
                result.ShouldNotHaveValidationErrorFor(x => x.ExpirationMinutesRange);
                _notesSessionDtoValidator.ShouldHaveChildValidator(x => x.Notes, typeof(NoteDtoValidator));
            }
        }
    }
}
