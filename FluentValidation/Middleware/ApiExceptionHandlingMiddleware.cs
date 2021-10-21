using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Validation.Enums;
using Validation.Infrastructure.Domain;
using Validation.Infrastructure.ProblemDetail;

namespace Validation.Middleware
{
    public class ApiExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionHandlingMiddleware> _logger;

        public ApiExceptionHandlingMiddleware(RequestDelegate next, ILogger<ApiExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                 await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            string result;

            CustomValidationProblemDetails problemDetail = null;
          
            if (ex is CustomException e)
            {                            
                var listErros = new List<ValidationError> { new() { Code = e.Code, Message = e.Message } };

                problemDetail = new CustomValidationProblemDetails(listErros, context.Request.Path, "Informação incompleta fornecida pelo cliente", (int)HttpStatusCode.BadRequest);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
             
            }
            else
            {
                _logger.LogError(ex.Message, $"Ocorreu uma exceção não tratada");

                var listErros = new List<ValidationError> { new() { Code = ErrorCode.Internal , Message = ex.Message } };

                problemDetail = new CustomValidationProblemDetails(listErros,context.Request.Path, "Erro interno do servidor", (int)HttpStatusCode.InternalServerError);
         
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            result = JsonSerializer.Serialize(problemDetail);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(result);
        }
    }
}
