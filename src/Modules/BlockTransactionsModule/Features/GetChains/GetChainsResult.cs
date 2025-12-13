using IcTest.Data.Dtos;
using IcTest.Shared.ApiResponses;

namespace BlockTransactionsModule.Features.GetChains
{
    public class GetChainsResult(List<BlockChainDto> blockChains) : ApiResponse<List<BlockChainDto>>(blockChains)
    {
    }
}
