using IcTest.Shared.Repositories.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IcTest.Shared.Repositories
{
    public abstract class RepositoryBase<T, TC> : IRepositoryBase<T> where T : class
                                                                     where TC : DbContext
    {
        protected TC RepositoryContext;
        public RepositoryBase(TC repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }
        #region Query Methods
        public IQueryable<T> FindAll(bool trackChanges)
        {
            if (trackChanges)
            {
                return RepositoryContext.Set<T>();
            }
            else
            {
                return RepositoryContext.Set<T>().AsNoTracking();
            }
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            if (trackChanges)
            {
                return RepositoryContext.Set<T>().Where(expression);
            }
            else
            {
                return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
            }
        }
        #endregion

        #region CRUD Methods
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }

        public void CreateRange(IList<T> entities)
        {
            RepositoryContext.Set<T>().AddRange(entities);
        }
        public async Task CreateRangeAsync(IList<T> entities)
        {
            await RepositoryContext.Set<T>().AddRangeAsync(entities);
        }
        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
        #endregion

        #region Getters
        public T? GetById(long id, bool trackChanges)
        {
            return FindByCondition(c => EF.Property<long>(c, "Id") == id, trackChanges).FirstOrDefault();
        }

        public async Task<T?> GetByIdAsync(long id, bool trackChanges)
        {
            return await FindByCondition(c => EF.Property<long>(c, "Id") == id, trackChanges).FirstOrDefaultAsync();
        }
        #endregion
    }
}
