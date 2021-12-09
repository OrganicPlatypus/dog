using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace domesticOrganizationGuru.Validation.Registration
{
    public static class ValidatorsRegistration
    {
        public static void RegisterValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(validator =>
                validator.RegisterValidatorsFromAssemblyContaining<CreateNotesPackDtoValidator>());
        }
    }
}
