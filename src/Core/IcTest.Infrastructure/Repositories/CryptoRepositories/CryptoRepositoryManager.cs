using IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts;
using IcTest.Infrastructure.Repositories.CryptoRepositories.Decorators;
using IcTest.Infrastructure.Services.Cache;

namespace IcTest.Infrastructure.Repositories.CryptoRepositories
{
    public class CryptoRepositoryManager : ICryptoRepositoryManager
    {
        protected CryptoDbContext CryptoDbContext { get; }
        #region Private Repos
        private readonly Lazy<IBlockChainRepository> _blockChainRepository;
        private readonly Lazy<IBlockHashRepository> _blockHashRepository;
        private readonly Lazy<IBlockTransactionRepository> _blockTransactionRepository;
        #endregion
        public CryptoRepositoryManager(ICacheService cacheService, CryptoDbContext cryptoDbContext)
        {
            CryptoDbContext = cryptoDbContext;
            _blockChainRepository = new Lazy<IBlockChainRepository>(() => new CachedBlockChainRepository(new BlockChainRepository(CryptoDbContext), cacheService));
            _blockHashRepository = new Lazy<IBlockHashRepository>(() => new CachedBlockHashRepository(new BlockHashRepository(CryptoDbContext), cacheService));
            _blockTransactionRepository = new Lazy<IBlockTransactionRepository>(() => new BlockTransactionRepository(CryptoDbContext));
        }

        #region Public Repos
        public IBlockChainRepository BlockChainRepository => _blockChainRepository.Value;
        public IBlockHashRepository BlockHashRepository => _blockHashRepository.Value;
        public IBlockTransactionRepository BlockTransactionRepository => _blockTransactionRepository.Value;
        #endregion

        #region Methods
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await CryptoDbContext.SaveChangesAsync(cancellationToken);
        public int SaveChanges(CancellationToken cancellationToken = default) => CryptoDbContext.SaveChanges();
        #endregion
    }
}
