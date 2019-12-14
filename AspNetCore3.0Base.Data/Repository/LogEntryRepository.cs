using AspNetCore3Base.CrossCutting.Interface.Repositories;
using AspNetCore3Base.Data.Context;
using AspNetCore3Base.Domain.Entities;


namespace AspNetCore3Base.Data.Repository
{
    public class LogEntryRepository : RepositoryBase<LogEntry>, ILoggingCrossCutting
    {
        public LogEntryRepository(ApplicationNameContext db)
            : base(db)
        {
        }
    }
}
