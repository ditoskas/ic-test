using System.Linq.Expressions;

namespace IcTest.Shared.Repositories.Contacts
{
    public interface IRepositoryBase<T>
    {
        #region Query Methods
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        #endregion

        #region CRUD Methods
        void Create(T entity);
        Task CreateAsync(T entity);
        void CreateRange(IList<T> entities);
        Task CreateRangeAsync(IList<T> entities);
        void Update(T entity);
        void Delete(T entity);
        #endregion

        #region Getters
        T? GetById(long id, bool trackChanges);
        Task<T?> GetByIdAsync(long id, bool trackChanges, CancellationToken cnt = default);

        Task<List<T>> GetPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize,
            CancellationToken cnt = default);

        #endregion
    }
}
