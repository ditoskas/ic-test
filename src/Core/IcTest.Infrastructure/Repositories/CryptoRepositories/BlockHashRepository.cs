using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Shared.Repositories;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories
{
    public class BlockHashRepository(CryptoDbContext cryptoDbContext) : RepositoryBase<BlockHash, CryptoDbContext>(cryptoDbContext), IBlockHashRepository
    {
    }
}
