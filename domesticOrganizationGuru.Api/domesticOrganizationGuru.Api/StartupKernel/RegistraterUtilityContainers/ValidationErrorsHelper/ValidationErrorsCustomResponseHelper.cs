using domesticOrganizationGuru.Validation.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DomesticOrganizationGuru.Api.StartupKernel.RegistraterUtilityContainers.ValidationErrorsHelper
{
    public static class ValidationErrorsCustomResponseHelper
    {
        public static IActionResult ValidationErrorsResponse(ActionContext actionContext)
        {
            var errors = string.Join('\n', actionContext.ModelState.Values.Where(v => v.Errors.Count > 0)
              .SelectMany(v => v.Errors)
              .Select(v => v.ErrorMessage));

            return new BadRequestObjectResult(errors);
        }
    }
}
