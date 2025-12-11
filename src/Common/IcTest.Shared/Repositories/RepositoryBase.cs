using IcTest.Shared.Repositories.Contacts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IcTest.Shared.ApiResponses;
using Mapster;

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

        public async Task<T?> GetByIdAsync(long id, bool trackChanges, CancellationToken cnt = default)
        {
            return await FindByCondition(c => EF.Property<long>(c, "Id") == id, trackChanges).FirstOrDefaultAsync(cnt);
        }

        public async Task<List<T>> GetPagedListAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cnt = default)
        {
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cnt).ConfigureAwait(false);
        }

        public async Task<PaginatedResult<TDto>> GetPaginateResultListAsync<TDto>(IQueryable<T> query, int pageNumber,
            int pageSize, CancellationToken cnt = default)
        {

            int totalRecords = await query.CountAsync(cnt).ConfigureAwait(false);
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            List<T> payload = await GetPagedListAsync(query, pageNumber, pageSize, cnt);
            List<TDto> dtoList = payload.Adapt<List<TDto>>();
            return new PaginatedResult<TDto>(pageNumber, pageSize, totalPages, dtoList);
        }
        #endregion
    }
}
