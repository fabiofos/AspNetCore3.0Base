using AspNetCore3._0Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore3._0Base.CrossCutting.Interface.Repositories
{
    public interface ILoggingCrossCutting
    {
        LogEntry Add(LogEntry Obj);

        Task<LogEntry> AddAsync(LogEntry Obj);

        IEnumerable<LogEntry> GetBy(Expression<Func<LogEntry, bool>> predicate, params Expression<Func<LogEntry, object>>[] includes);

        LogEntry GetFirstBy(Expression<Func<LogEntry, bool>> predicate, params Expression<Func<LogEntry, object>>[] includes);

        LogEntry GetById(int Id);

        IEnumerable<LogEntry> GetAll();

        LogEntry Update(LogEntry Obj);

        Task<LogEntry> UpdateAsync(LogEntry Obj);

        void Remove(LogEntry Obj);

        Task<int> RemoveAsync(LogEntry Obj);

        IEnumerable<LogEntry> GetPagedRecords(Expression<Func<LogEntry, bool>> predicate, Expression<Func<LogEntry, string>> orderBy, int pageNo, int pageSize);


    }
}
