using AspNetCore3Base.Data.Context;
using AspNetCore3Base.Data.Contract;
using AspNetCore3Base.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore3Base.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly ApplicationNameContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryBase(ApplicationNameContext db)
        {
            _db = db;
            _unitOfWork = new UnitOfWork(db);
        }
        public TEntity Add(TEntity Obj)
        {
            _db.Set<TEntity>().Add(Obj);
            _db.SaveChanges();
            return Obj;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _db.Set<TEntity>().AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _db.Set<TEntity>().AsQueryable().AsNoTracking();

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.Where(predicate).ToList();
        }

        public TEntity GetFirstBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _db.Set<TEntity>().AsQueryable().AsNoTracking();

            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.OrderByDescending(predicate).FirstOrDefault(predicate);
        }

        public TEntity GetById(int Id)
        {
            return _db.Set<TEntity>().Find(Id);
        }

        public IEnumerable<TEntity> GetPagedRecords(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, string>> orderBy, int pageNo, int pageSize)
        {
            return (_db.Set<TEntity>().Where(predicate).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        public void Remove(TEntity Obj)
        {
            _db.Set<TEntity>().Remove(Obj);
            _db.SaveChanges();
        }

        public TEntity Update(TEntity Obj)
        {
            if (Obj == null)
            {
                return null;
            }

            _db.Set<TEntity>().Attach(Obj);
            _db.Entry(Obj).State = EntityState.Modified;
            _db.SaveChanges();

            return Obj;
        }

        public async Task<TEntity> AddAsync(TEntity Obj)
        {
            _db.Set<TEntity>().Add(Obj);
            await _unitOfWork.Commit();
            return Obj;
        }

        public async Task<TEntity> UpdateAsync(TEntity Obj)
        {
            if (Obj == null)
            {
                return null;
            }

            _db.Set<TEntity>().Attach(Obj);
            _db.Entry(Obj).State = EntityState.Modified;
            await _unitOfWork.Commit();

            return Obj;
        }

        public async Task<int> RemoveAsync(TEntity Obj)
        {
            _db.Set<TEntity>().Remove(Obj);
            return await _unitOfWork.Commit();
        }
    }
}
