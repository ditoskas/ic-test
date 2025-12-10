using IcTest.Shared.Models;

namespace IcTest.Data.Models
{
    public class BlockHash : EntityWithTimestamps
    {
        public required string Hash { get; set; }
        public int Height { get; set; }
        public required string Chain { get; set; }
        public long Total { get; set; }
        public long Fees { get; set; }
        public int? Size { get; set; }
        public int? Vsize { get; set; }
        public int Ver { get; set; }
        public DateTime Time { get; set; }
        public DateTime ReceivedTime { get; set; }
        public string CoinbaseAddr { get; set; } = string.Empty;
        public string RelayedBy { get; set; } = string.Empty;
        public long Bits { get; set; }
        public long Nonce { get; set; }
        public int NTx { get; set; }
        public string PrevBlock { get; set; } = string.Empty;
        public string MrklRoot { get; set; } = string.Empty;
        public List<BlockTransaction> Txids { get; set; } = new List<BlockTransaction>();
        public int Depth { get; set; }
        public string PrevBlockUrl { get; set; } = string.Empty;
        public string TxUrl { get; set; } = string.Empty;
        public string? NextTxids { get; set; }
    }
}
