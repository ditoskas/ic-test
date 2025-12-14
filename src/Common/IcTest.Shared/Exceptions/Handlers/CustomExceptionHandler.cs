using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace IcTest.Shared.Exceptions.Handlers
{
    /// <summary>
    /// Middleware for handling API exceptions.  
    /// Logs exception details and returns custom error responses for known exceptions, while generic responses are given for unknown ones.
    /// </summary>
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            //var requestDetails = await RequestDetailsHelper.CaptureAsync(httpContext, logger, cancellationToken);

            //// Create a log message with exception and inner exception details
            //string logMessage = CreateLogMessage(exception);

            httpContext.Response.ContentType = "application/json";
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature == null) return true;

            //Transform to IcTest Exception
            IcTestException icTestException = exception switch
            {
                ValidationException fluentException => new BadRequestException(fluentException),
                BadHttpRequestException badHttpRequestException => new BadRequestException(badHttpRequestException.Message),
                _ => new InternalServerException(exception.Message)
            };
            icTestException.TraceId = httpContext.TraceIdentifier;
            httpContext.Response.StatusCode = (int)icTestException.StatusCode;


            // Log as an error for 5xx, warning otherwise
            if (icTestException is InternalServerException)
            {
                logger.LogError(icTestException, "Unknown error");
            }
            else
            {
                logger.LogWarning(icTestException, "Validation warning");
            }
            // Return custom error response
            await httpContext.Response.WriteAsync(icTestException.ToApiJsonResponse(), cancellationToken);
            return true;
        }
    }
}
