using IcTest.Data.Dtos;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.ApiResponses;
using IcTest.Shared.CQRS;
using IcTest.Shared.Helpers;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionHandler(ICryptoRepositoryManager cryptoRepositoryManager) : IQueryHandler<GetTransactionQuery, GetTransactionResult>
    {
        public async Task<GetTransactionResult> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            string chain = CryptoUtils.GetCoinWithChain(request.CoinId, request.ChainId);
            PaginatedResult<BlockHashDto> transactions =
                await cryptoRepositoryManager.BlockHashRepository.GetHistoryAsync(chain, request.Page, request.PageSize,
                    false, cancellationToken);
            return new GetTransactionResult(transactions);
        }
    }
}
