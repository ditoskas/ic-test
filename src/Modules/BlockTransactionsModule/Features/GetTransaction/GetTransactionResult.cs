using IcTest.Shared.ApiResponses;
using IcTest.Shared.Dto.CryptoDto;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionResult(PaginatedResult<TransactionDto> transactions) : ApiPaginatedResponse<TransactionDto>(transactions)
    {
    }
}
