using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Validation.Filters;

namespace Validation.Infrastructure.ProblemDetail
{
    public class CustomProblemDetailsFactory : ProblemDetailsFactory
    {
        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string title = null,
            string type = null, string detail = null, string instance = null)
        {
          return new CustomValidationProblemDetails(instance, title, statusCode);
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext,
            ModelStateDictionary modelStateDictionary, int? statusCode = null, string title = null, string type = null,
            string detail = null, string instance = null)
        {
            statusCode ??= 400;
            instance ??= httpContext.Request.Path;
            title ??= "Input Model incorreto";

            CustomValidationProblemDetails problemDetail = new CustomValidationProblemDetails(modelStateDictionary, instance, title, statusCode);

            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
             {
                problemDetail.Extensions["traceId"] = traceId;
            }

            return problemDetail;
        }
    }
}
