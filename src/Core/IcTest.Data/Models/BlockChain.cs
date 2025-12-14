using IcTest.Shared.Helpers;
using IcTest.Shared.Models;

namespace IcTest.Data.Models
{
    public class BlockChain : EntityWithTimestamps
    {
        public required string Coin { get; set; }
        public required string Chain { get; set; }
        public string Name => CryptoUtils.GetCoinWithChain(Coin, Chain);
    }
}
