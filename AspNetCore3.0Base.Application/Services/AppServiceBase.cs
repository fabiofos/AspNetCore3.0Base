using AspNetCore3Base.Application.Interface;
using AspNetCore3Base.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore3Base.Application.Services
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public TEntity Add(TEntity Obj)
        {
            return _serviceBase.Add(Obj);
        }

        public Task<TEntity> AddAsync(TEntity Obj)
        {
            return _serviceBase.AddAsync(Obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return _serviceBase.GetBy(predicate, includes);
        }

        public TEntity GetById(int Id)
        {
            return _serviceBase.GetById(Id);
        }

        public TEntity GetFirstBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return _serviceBase.GetFirstBy(predicate, includes);
        }

        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize)
        {
            return _serviceBase.GetPagedRecords(predicate, orderBy, pageNo, pageSize);
        }

        public void Remove(TEntity Obj)
        {
           _serviceBase.Remove(Obj);
        }

        public Task<int> RemoveAsync(TEntity Obj)
        {
            return _serviceBase.RemoveAsync(Obj);
        }

        public TEntity Update(TEntity Obj)
        {
            return _serviceBase.Update(Obj);
        }

        public Task<TEntity> UpdateAsync(TEntity Obj)
        {
            return _serviceBase.UpdateAsync(Obj);
        }
    }
}
