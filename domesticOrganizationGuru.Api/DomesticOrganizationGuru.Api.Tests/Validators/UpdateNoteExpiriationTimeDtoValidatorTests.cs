using domesticOrganizationGuru.Common.Dto;
using domesticOrganizationGuru.Validation;
using DomesticOrganizationGuru.Api.Tests.TestData;
using FluentValidation.TestHelper;
using Xunit;
using static FluentValidation.TestHelper.ValidationTestExtension;

namespace DomesticOrganizationGuru.Api.Tests
{
    public class UpdateNoteExpiriationTimeDtoValidatorTests
    {
        private UpdateNoteExpiriationTimeDtoValidator _updateExpiriationTimeValidator;
        public UpdateNoteExpiriationTimeDtoValidatorTests()
        {
            _updateExpiriationTimeValidator = new();
        }

        [Fact]
        public void UpdateNoteRequestDtoValidatorTests_Success_Tests()
        {
            // Arrange
            UpdateNoteExpiriationTimeDto updateExpiriationTimeDto = new()
            {
                ExpirationMinutesRange = 1,
                NoteName = "Correct Name"
            };

            // Act
            var result = _updateExpiriationTimeValidator.TestValidate(updateExpiriationTimeDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ExpirationMinutesRange);
            result.ShouldNotHaveValidationErrorFor(x => x.NoteName);
        }

        [Theory]
        [ClassData(typeof(UpdateNoteExpiriationTimeDtoTestsFailuresData))]
        public void UpdateNoteRequestDtoValidatorTests_Failures_Tests(UpdateNoteExpiriationTimeDto updateExpiriationTimeDto)
        {
            // Arrange

            // Act
            var result = _updateExpiriationTimeValidator.TestValidate(updateExpiriationTimeDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ExpirationMinutesRange);
            result.ShouldHaveValidationErrorFor(x => x.NoteName);
        }
    }
}
