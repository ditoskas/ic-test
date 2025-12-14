using IcTest.Shared.Models;

namespace IcTest.Data.Models
{
    public class BlockTransaction : EntityWithTimestamps
    {
        public required string Hash { get; set; }
        public long BlockHashId { get; set; }
        public BlockHash? BlockHash { get; set; }
    }
}
