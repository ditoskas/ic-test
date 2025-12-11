using FluentValidation;
using System.Net;

namespace IcTest.Shared.Exceptions
{
    /// <summary>
    /// Base class for bad request exceptions
    /// </summary>
    public class BadRequestException : IcTestException
    {
        public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
        {
        }

        public BadRequestException(ValidationException fluentException):base(fluentException.Errors.FirstOrDefault()?.ErrorMessage ?? fluentException.Message, HttpStatusCode.BadRequest)
        {
            Errors = fluentException.Errors;
        }
    }
}
