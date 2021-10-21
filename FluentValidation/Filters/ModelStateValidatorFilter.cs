using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Validation.Infrastructure.Domain;
using Validation.Infrastructure.ProblemDetail;

namespace Validation.Filters
{
    public class ModelStateValidatorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var listErroMessage = context.ModelState
                    .SelectMany(ms => ms.Value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                var customErros = listErroMessage
                    .Select(message => new CustomException(message, Enums.ErrorCode.ValidationInputView))
                    .ToList();

                var errors = new ErrosViewModel(customErros);

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }




}
