using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace domesticOrganizationGuru.Validation.Registration
{
    public static class ValidatorsRegistration
    {
        public static void RegisterValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(validator =>
                validator.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
