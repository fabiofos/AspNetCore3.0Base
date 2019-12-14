using AspNetCore3Base.CrossCutting.Interface.Repositories;
using AspNetCore3Base.Data.Context;
using AspNetCore3Base.Domain.Entities;


namespace AspNetCore3Base.Data.Repository
{
    public class LogEntryWebApiRepository : RepositoryBase<LogEntryWebApi>, ILoggingWebApiCrossCutting
    {
        public LogEntryWebApiRepository(ApplicationNameContext db)
            : base(db)
        {
        }
    }
}
