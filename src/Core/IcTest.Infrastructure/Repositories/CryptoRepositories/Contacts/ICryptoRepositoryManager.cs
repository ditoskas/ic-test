namespace IcTest.Infrastructure.Repositories.CryptoRepositories.Contacts
{
    public interface ICryptoRepositoryManager
    {
        CryptoDbContext CryptoDbContext { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges(CancellationToken cancellationToken = default);
    }
}
