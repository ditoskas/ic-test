namespace IcTest.Shared.Helpers
{
    public class CryptoUtils
    {
        /// <summary>
        /// Get the coin with chain format as BlockCypher uses, e.g., BTC.main
        /// </summary>
        /// <param name="coinId"></param>
        /// <param name="chainId"></param>
        /// <returns></returns>
        public static string GetCoinWithChain(string coinId, string chainId)
        {
            return $"{coinId.ToUpperInvariant()}.{chainId.ToLowerInvariant()}";
        }
    }
}
