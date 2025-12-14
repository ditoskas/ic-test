using IcTest.Shared.ApiResponses;
using System.Net;

namespace IcTest.Shared.Exceptions
{
    public interface IIcTestException
    {
        HttpStatusCode StatusCode { get; set; }
        string TraceId { get; set; }
        ApiResponse<string> ToApiResponse();
        string ToApiJsonResponse();
    }
}
