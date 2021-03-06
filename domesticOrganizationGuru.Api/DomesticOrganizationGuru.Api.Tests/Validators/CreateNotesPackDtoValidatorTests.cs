using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Common.Resources.Validation;
using domesticOrganizationGuru.Validation;
using DomesticOrganizationGuru.Api.Tests.TestData;
using FluentValidation.TestHelper;
using Xunit;

namespace DomesticOrganizationGuru.Api.Tests
{
    public class CreateNotesPackDtoValidatorTests
    {
        private CreateNotesPackDtoValidator _createNotesPackDtoValidator;
        public CreateNotesPackDtoValidatorTests()
        {
            _createNotesPackDtoValidator = new();
        }
        [Fact]
          public void CreateNotesPackDtoValidatorTests_Success_Test()
        {
            // Arrange
            CreateNoteDto createNotesPackDto = new()
            {
                ExpirationMinutesRange = 1,
                NoteName = "NoteName"
            };

            // Act
            var result = _createNotesPackDtoValidator.TestValidate(createNotesPackDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ExpirationMinutesRange);
            result.ShouldNotHaveValidationErrorFor(x => x.NoteName);
        }

        [Theory]
        [ClassData(typeof(CreateNotesPackDtoValidatorTestsFailuresData))]
        public void CreateNotesPackDtoValidatorTests_Failures_Test(CreateNoteDto createNotesPackDto, bool faultyExpiriation, bool faultyName)
        {
            // Arrange
            // Act
            var result = _createNotesPackDtoValidator.TestValidate(createNotesPackDto);

            // Assert
            if (faultyExpiriation)
            {
            result.ShouldHaveValidationErrorFor(x => x.ExpirationMinutesRange)
                    .WithErrorMessage(ValidationMessages.LifetimeInformation);
            }
            if (faultyName)
            {
            result.ShouldHaveValidationErrorFor(x => x.NoteName)
                    .WithErrorMessage(ValidationMessages.ProvideNameInformation);
            }
        }
    }
}
