using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using domesticOrganizationGuru.Validation;
using DomesticOrganizationGuru.Api.Tests.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests
{
    public class NoteDtoValidatorTests
    {
        private NoteDtoValidator _noteDtoValidator;
        public NoteDtoValidatorTests()
        {
            _noteDtoValidator = new();
        }
        [Fact]
          public void CreateNotesPackDtoValidatorTests_Success_Test()
        {
            // Arrange
            NoteDto createNoteDto = new()
            {
                NoteText = "New note",
                IsComplete = true,
            };

            // Act
            var result = _noteDtoValidator.TestValidate(createNoteDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.NoteText);
            result.ShouldNotHaveValidationErrorFor(x => x.IsComplete);
        }

        [Theory]
        [ClassData(typeof(NoteDtoValidatorTestsFailuresData))]
        public void CreateNotesPackDtoValidatorTests_Failures_Test(NoteDto createNotesPackDto, string expectedErrorMessage)
        {
            // Arrange
            
            // Act
            var result = _noteDtoValidator.TestValidate(createNotesPackDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.NoteText)
                    .WithErrorMessage(expectedErrorMessage);
        }
    }
}
