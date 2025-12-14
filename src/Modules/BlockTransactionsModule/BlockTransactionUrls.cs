namespace BlockTransactionsModule
{
    public abstract class BlockTransactionUrls
    {
        public const string CryptoTransactionsHistory = "/crypto/transactions/{coinId}/{chainId}";
        public const string CryptoChains = "/crypto/chains";
    }
}
