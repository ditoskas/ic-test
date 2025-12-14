namespace BlockCypher.Data
{
    public abstract class BlockCypherEndPoints
    {
        public const string ChainInfo = "/v1/{0}/{1}";
        public const string BlockByHash = "/v1/{0}/{1}/blocks/{2}";
    }
}
