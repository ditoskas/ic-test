using FluentValidation.Results;
using IcTest.Shared.ApiResponses;
using System.Net;
using System.Text.Json;

namespace IcTest.Shared.Exceptions
{
    public abstract class IcTestException(string message, HttpStatusCode responseCode = HttpStatusCode.InternalServerError) : Exception(message), IIcTestException
    {
        public HttpStatusCode StatusCode { get; set; } = responseCode;
        public string TraceId { get; set; } = string.Empty;
        protected IEnumerable<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public virtual ApiResponse<string> ToApiResponse()
        {
            if (Errors.Any())
            {
                return new ApiResponse<string>(Errors);
            }
            return new ApiResponse<string>(Message);
        }
        /// <summary>
        /// Transform the exception to ApiResponse and return the json
        /// </summary>
        /// <returns></returns>
        public virtual string ToApiJsonResponse()
        {
            return ToApiResponse().ToString();
        }
    }
}
