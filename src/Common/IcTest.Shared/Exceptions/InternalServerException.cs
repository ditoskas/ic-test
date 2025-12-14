using System.Net;

namespace IcTest.Shared.Exceptions
{
    /// <summary>
    /// Base class for internal server exceptions
    /// </summary>
    public class InternalServerException(string message) : IcTestException(message, HttpStatusCode.InternalServerError)
    {
    }
}
