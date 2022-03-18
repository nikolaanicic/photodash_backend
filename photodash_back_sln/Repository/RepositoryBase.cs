using Contracts.RepositoryBase;
using Entities.RepoContext;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T:class
    {

        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext dbContext)
        {
            RepositoryContext = dbContext;
        }


        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
            
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ? RepositoryContext.Set<T>() : RepositoryContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? RepositoryContext.Set<T>().Where<T>(expression) : RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> FindByConditionPaged(Expression<Func<T, bool>> expression, RequestParameters requestParams,bool trackChanges)
        {
            return trackChanges ? RepositoryContext.Set<T>().Where<T>(expression).Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
                .Take(requestParams.PageSize) :
                RepositoryContext.Set<T>().Where(expression).Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
                .Take(requestParams.PageSize);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }
    }
}
