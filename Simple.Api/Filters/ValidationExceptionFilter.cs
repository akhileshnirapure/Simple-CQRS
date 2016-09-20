using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using FluentValidation;

namespace Simple.Api.Filters
{
    public class ValidationExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var validationException = actionExecutedContext.Exception as ValidationException;
            if (validationException != null)
            {
                var errors = validationException.Errors
                    .Select(x => new
                    {
                        field = x.PropertyName,
                        errorMessage = x.ErrorMessage,
                        value = x.AttemptedValue,
                    });

                var response = new { ValidationErrors = errors };

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
    }
}