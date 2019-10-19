using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore3._0Base.Application.Interface
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        TEntity Add(TEntity Obj);

        Task<TEntity> AddAsync(TEntity Obj);

        IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        TEntity GetFirstBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        TEntity GetById(int Id);

        IEnumerable<TEntity> GetAll();

        TEntity Update(TEntity Obj);

        Task<TEntity> UpdateAsync(TEntity Obj);

        void Remove(TEntity Obj);

        Task<int> RemoveAsync(TEntity Obj);

        IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize);

    }
}
