using System;
using Validation.Enums;

namespace Validation.Infrastructure.Domain
{
    public class CustomException : Exception
    {

        public CustomException(string message, ErrorCode code = ErrorCode.Internal)
            : base(message)
        {
            Code = code;
        }

        public ErrorCode Code { get; }
    }
}
