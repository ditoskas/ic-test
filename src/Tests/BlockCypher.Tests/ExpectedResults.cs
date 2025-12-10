using BlockCypher.Data.Models;

namespace BlockCypher.Tests
{
    public class ExpectedResults
    {
        public static BlockCypherChain BtcMainChain => new BlockCypherChain
        {
            Name = "BTC.main",
            Height = 927138,
            Hash = "0000000000000000000102aa51f848dff73f0fedb75b151fe0d8571709bcc77e",
            LatestUrl = "https://api.blockcypher.com/v1/btc/main/blocks/0000000000000000000102aa51f848dff73f0fedb75b151fe0d8571709bcc77e",
            PreviousHash = "00000000000000000001465bd9384c32f8cd2ee7d7272dda6e5c9e8061bad465",
            PreviousUrl = "https://api.blockcypher.com/v1/btc/main/blocks/00000000000000000001465bd9384c32f8cd2ee7d7272dda6e5c9e8061bad465"
        };

        public static BlockCypherBlockHash BtcBlockHash = new BlockCypherBlockHash()
        {
            Hash = "00000000000000000003dc20b868d17121303308f6bba329302e75913f0790db",
            Chain = "BTC.main",
            Height = 671142,
            Nonce = 3270005482
        };
    }
}
