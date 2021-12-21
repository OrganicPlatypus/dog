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

            if (context.Exception is NoteNotFoundException noteNotFoundException)
            {
                context.Result = new ObjectResult(noteNotFoundException.NoteName)
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Value = noteNotFoundException.Message
                };

                context.ExceptionHandled = true;
            }

            if (context.Exception is NotAbleToDistributeToGroupException notDistributedException)
            {
                context.Result = new ObjectResult(notDistributedException.NoteName)
                {
                    StatusCode = StatusCodes.Status503ServiceUnavailable,
                    Value = notDistributedException.Message
                };

                context.ExceptionHandled = true;
            }

            if (context.Exception is UpdateNotesException updateException)
            {
                context.Result = new ObjectResult(updateException.NoteName)
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Value = updateException.Message
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
