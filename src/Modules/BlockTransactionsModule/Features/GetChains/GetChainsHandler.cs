using IcTest.Data.Dtos;
using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.CQRS;
using Mapster;

namespace BlockTransactionsModule.Features.GetChains
{
    public class GetChainsHandler(ICryptoRepositoryManager cryptoRepositoryManager) : IQueryHandler<GetChainsQuery, GetChainsResult>
    {
        public async Task<GetChainsResult> Handle(GetChainsQuery request, CancellationToken cancellationToken)
        {
            List<BlockChain> activeChains = await cryptoRepositoryManager.BlockChainRepository.GetActiveChainsAsync(false, cancellationToken);
            List<BlockChainDto> payload = activeChains.Adapt<List<BlockChainDto>>();
            return new GetChainsResult(payload);
        }
    }
}
