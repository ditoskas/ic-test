using IcTest.Shared.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace IcTest.Shared.BackgroundServices
{
    public class BackgroundServiceHealthCheck (string serviceStatus): IHealthCheck
    {
        public const string HealthyStatus = BackgroundServiceStatus.Running;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (serviceStatus == HealthyStatus)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Background service is healthy"));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("Background service is not healthy"));
            }
        }
    }
}