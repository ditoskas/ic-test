using IcTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts
{
    public interface ICryptoDbContext
    {
        public DbSet<BlockChain> BlockChains { get; }
        public DbSet<BlockHash> BlockHashes { get; }
        public DbSet<BlockTransaction> BlockTransactions { get; }
    }
}
