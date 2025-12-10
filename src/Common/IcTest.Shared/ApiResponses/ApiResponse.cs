using System.Text.Json;
using FluentValidation.Results;

namespace IcTest.Shared.ApiResponses
{
    /// <summary>
    /// Standardization of API responses
    /// </summary>
    /// <typeparam name="T">The payload class type</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public List<ErrorRecord> Messages { get; set; }
        public T? Payload { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        /// <summary>
        /// Success scenario
        /// </summary>
        /// <param name="payload"></param>
        public ApiResponse(T payload):this(true, payload, [])
        {
        }
        /// <summary>
        /// Empty response with just success status
        /// </summary>
        /// <param name="success"></param>
        public ApiResponse(bool success = true) : this(success, default(T)!, [])
        {
        }
        /// <summary>
        /// Generic error not tied to a specific field
        /// </summary>
        /// <param name="error"></param>
        public ApiResponse(string error) : this(false, default(T)!, [new ErrorRecord("general", error)])
        {
        }
        /// <summary>
        /// For fluent validation errors
        /// </summary>
        /// <param name="errors"></param>
        public ApiResponse(IEnumerable<ValidationFailure> errors)
        {
            Success = false;
            Payload = default;
            Messages = errors.Select(e => new ErrorRecord(e.PropertyName, e.ErrorMessage)).ToList();
        }
        /// <summary>
        /// Main Constructor
        /// </summary>
        /// <param name="success"></param>
        /// <param name="payload"></param>
        /// <param name="errors"></param>
        public ApiResponse(bool success, T payload, IEnumerable<ErrorRecord> errors)
        {
            Success = success;
            Payload = payload;
            Messages = errors.ToList();
        }
    }
}
