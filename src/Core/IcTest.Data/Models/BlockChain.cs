using IcTest.Shared.Models;

namespace IcTest.Data.Models
{
    public class BlockChain : EntityWithTimestamps
    {
        public required string Name { get; set; }
        public int Height { get; set; }
        public required string Hash { get; set; }
        public DateTime Time { get; set; }
        public required string LatestUrl { get; set; }
        public required string PreviousHash { get; set; }
        public required string PreviousUrl { get; set; }
        public int PeerCount { get; set; }
        public int UnconfirmedCount { get; set; }
        public int HighFeePerKb { get; set; }
        public int MediumFeePerKb { get; set; }
        public int LowFeePerKb { get; set; }
        public int? LastForkHeight { get; set; }
        public string? LastForkHash { get; set; }
    }
}
