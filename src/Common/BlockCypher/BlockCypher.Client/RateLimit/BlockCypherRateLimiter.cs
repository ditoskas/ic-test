using System.Threading.RateLimiting;

namespace BlockCypher.Client.RateLimit
{
    /// <summary>
    /// Below is the BlockCypher rate limiter implementation based on Fixed Window algorithm.
    /// @url: https://medium.com/@callmeyaz/rate-limiting-in-asp-net-core-9-0-a9f9d4256fd5
    /// </summary>
    public class BlockCypherRateLimiter
    {
        public const int LimitPerSecond = 3;
        public const int LimitPerHour = 100;

        public static readonly FixedWindowRateLimiter PerSecondLimiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = LimitPerSecond,
            Window = TimeSpan.FromSeconds(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = LimitPerHour
        });

        public static readonly FixedWindowRateLimiter PerHourLimiter = new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions
        {
            PermitLimit = LimitPerHour,
            Window = TimeSpan.FromHours(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = LimitPerHour
        });

        public static async ValueTask<RateLimitLease> AcquireAsync(
            CancellationToken ct)
        {
            // Acquire per-second first (fast-fail)
            var secondLease = await PerSecondLimiter.AcquireAsync(1, ct);
            if (!secondLease.IsAcquired)
                return secondLease;

            // Then acquire per-hour
            var hourLease = await PerHourLimiter.AcquireAsync(1, ct);
            if (!hourLease.IsAcquired)
            {
                secondLease.Dispose();
                return hourLease;
            }

            return new BlockCypherLease(secondLease, hourLease);
        }
    }
}
