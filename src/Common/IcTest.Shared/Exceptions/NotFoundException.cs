using System.Net;

namespace IcTest.Shared.Exceptions
{
    /// <summary>
    /// Base class for not found exceptions
    /// </summary>
    public class NotFoundException(string message) : IcTestException(message, HttpStatusCode.NotFound);
}
