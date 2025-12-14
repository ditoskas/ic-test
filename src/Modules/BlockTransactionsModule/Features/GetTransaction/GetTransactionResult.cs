using IcTest.Data.Dtos;
using IcTest.Shared.ApiResponses;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionResult(PaginatedResult<BlockHashDto> transactions) : ApiPaginatedResponse<BlockHashDto>(transactions)
    {
    }
}
