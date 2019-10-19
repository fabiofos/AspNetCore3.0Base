using AspNetCore3._0Base.Data.Context;
using AspNetCore3._0Base.Domain.Entities;
using AspNetCore3._0Base.Domain.Interfaces.Repositories;

namespace AspNetCore3._0Base.Data.Repository
{
    public class MenuPermissionRepository : RepositoryBase<MenuPermission>, IMenuPermissionRepository
    {
        public MenuPermissionRepository(ApplicationNameContext db)
            : base(db)
        {
        }
    }
}
