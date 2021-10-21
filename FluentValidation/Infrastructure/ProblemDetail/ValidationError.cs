using System.Text.Json.Serialization;
using Validation.Enums;

namespace Validation.Infrastructure.ProblemDetail
{
    public class ValidationError
    {
        
        [JsonPropertyName("code")]
        public ErrorCode Code { get; set; }


        [JsonPropertyName("message")]
        public string Message { get; set; }

    }
}
