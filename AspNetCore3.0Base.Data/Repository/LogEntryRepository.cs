using AspNetCore3._0Base.CrossCutting.Interface.Repositories;
using AspNetCore3._0Base.Data.Context;
using AspNetCore3._0Base.Domain.Entities;


namespace AspNetCore3._0Base.Data.Repository
{
    public class LogEntryRepository : RepositoryBase<LogEntry>, ILoggingCrossCutting
    {
        public LogEntryRepository(ApplicationNameContext db)
            : base(db)
        {
        }
    }
}
