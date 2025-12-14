using IcTest.Data.Models;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories
{
    public class CryptoDbContext(DbContextOptions<CryptoDbContext> options) : DbContext(options), ICryptoDbContext
    {
        public DbSet<BlockChain> BlockChains => Set<BlockChain>();
        public DbSet<BlockHash> BlockHashes => Set<BlockHash>();
        public DbSet<BlockTransaction> BlockTransactions => Set<BlockTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
