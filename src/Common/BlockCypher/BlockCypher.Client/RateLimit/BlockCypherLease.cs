using System.Threading.RateLimiting;

namespace BlockCypher.Client.RateLimit
{
    /// <summary>
    /// Logic used from below url to combine two leases into one.
    /// @url: https://devblogs.microsoft.com/dotnet/announcing-rate-limiting-for-dotnet/
    /// </summary>
    /// <param name="perSecondLease"></param>
    /// <param name="perHourLease"></param>
    public sealed class BlockCypherLease (RateLimitLease perSecondLease, RateLimitLease perHourLease): RateLimitLease
    {
        public override bool IsAcquired => true;

        public override IEnumerable<string> MetadataNames =>
            perSecondLease.MetadataNames.Concat(perHourLease.MetadataNames);

        public override bool TryGetMetadata(string metadataName, out object? metadata)
        {
            return perSecondLease.TryGetMetadata(metadataName, out metadata)
                   || perHourLease.TryGetMetadata(metadataName, out metadata);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                perSecondLease.Dispose();
                perHourLease.Dispose();
            }
        }
    }

}
