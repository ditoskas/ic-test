namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts
{
    public interface ICryptoRepositoryManager
    {
        #region Repositories
        public IBlockChainRepository BlockChainRepository { get; }
        public IBlockHashRepository BlockHashRepository { get; }
        public IBlockTransactionRepository BlockTransactionRepository { get; }
        #endregion

        #region Methods
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges(CancellationToken cancellationToken = default);
        #endregion
    }
}
