using IcTest.Shared.CQRS;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionQuery(string coinId, string chainId, int page, int pageSize) : IQuery<GetTransactionResult>
    {
        public string CoinId { get; } = coinId;
        public string ChainId { get; } = chainId;
        public int Page { get; } = page;
        public int PageSize { get; } = pageSize;
    }
}