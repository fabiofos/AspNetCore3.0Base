using AspNetCore3Base.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCore3Base.CrossCutting.Interface.Repositories
{
    public interface ILoggingWebApiCrossCutting
    {
        LogEntryWebApi Add(LogEntryWebApi Obj);

        Task<LogEntryWebApi> AddAsync(LogEntryWebApi Obj);

        IEnumerable<LogEntryWebApi> GetBy(Expression<Func<LogEntryWebApi, bool>> predicate, params Expression<Func<LogEntryWebApi, object>>[] includes);

        LogEntryWebApi GetFirstBy(Expression<Func<LogEntryWebApi, bool>> predicate, params Expression<Func<LogEntryWebApi, object>>[] includes);

        LogEntryWebApi GetById(int Id);

        IEnumerable<LogEntryWebApi> GetAll();

        LogEntryWebApi Update(LogEntryWebApi Obj);

        Task<LogEntryWebApi> UpdateAsync(LogEntryWebApi Obj);

        void Remove(LogEntryWebApi Obj);

        Task<int> RemoveAsync(LogEntryWebApi Obj);

        IEnumerable<LogEntryWebApi> GetPagedRecords(Expression<Func<LogEntryWebApi, bool>> predicate, Expression<Func<LogEntryWebApi, string>> orderBy, int pageNo, int pageSize);


    }
}
