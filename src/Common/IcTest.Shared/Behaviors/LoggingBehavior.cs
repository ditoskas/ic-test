using MediatR;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace IcTest.Shared.Behaviors
{
    /// <summary>
    /// This can be used as a pipeline behavior to log the request and response of the request.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="logger"></param>
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            string responseName = typeof(TResponse).Name;

            logger.LogInformation($"[START] Handle request={requestName} - Response={responseName} - RequestData={JsonSerializer.Serialize(request)}");

            var timer = new Stopwatch();
            timer.Start();

            var response = await next(cancellationToken);

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
                logger.LogWarning($"[PERFORMANCE] The request {requestName} took {timeTaken.Seconds} seconds.");

            logger.LogInformation($"[END] Handled {requestName} with {responseName}, Response Data = {JsonSerializer.Serialize(response)}");
            return response;
        }
    }
}
