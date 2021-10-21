using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Validation.Application.Validators;

namespace Validation.Infrastructure.ProblemDetail
{
    public class CustomValidationProblemDetails : ValidationProblemDetails
    {
        [JsonPropertyName("errors")]
        public new IEnumerable<ValidationError> Errors { get; } = new List<ValidationError>();

        public CustomValidationProblemDetails(string instance, string title, int? statusCode)
        {
            this.Status = statusCode;
            this.Instance = instance;
            this.Title = title;
        }

        public CustomValidationProblemDetails(IEnumerable<ValidationError> errors, string instance, string title, int? statusCode)
            : this(instance, title, statusCode)
        {
            Errors = errors;
        }

        public CustomValidationProblemDetails(ModelStateDictionary modelState, string instance, string title, int? statusCode)
            : this(instance, title, statusCode)
        {
            Errors = ConvertModelStateErrorsToValidationErrors(modelState);
        }

        private List<ValidationError> ConvertModelStateErrorsToValidationErrors(ModelStateDictionary modelStateDictionary)
        {
            List<ValidationError> validationErrors = new();

            foreach (var keyModelStatePair in modelStateDictionary)
            {
                var errors = keyModelStatePair.Value.Errors;
                switch (errors.Count)
                {
                    case 0:
                        continue;

                    case 1:
                        var erro = errors[0];
                        validationErrors.Add(new ValidationError { Code = Enums.ErrorCode.ValidationInputView, Message = erro.ErrorMessage });
                        break;

                    default:
                        var errorMessage = string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage));
                        validationErrors.Add(new ValidationError { Message = errorMessage });
                        break;
                }
            }

            return validationErrors;
        }

    }

}
