using domesticOrganizationGuru.Common.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DomesticOrganizationGuru.Api.StartupKernel
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is CreateNotesException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.NoteName)
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Value = httpResponseException.Message
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
