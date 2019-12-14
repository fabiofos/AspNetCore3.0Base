using AspNetCore3Base.Data.Context;
using AspNetCore3Base.Domain.Entities;
using AspNetCore3Base.Domain.Interfaces.Repositories;

namespace AspNetCore3Base.Data.Repository
{
    public class MenuPermissionRepository : RepositoryBase<MenuPermission>, IMenuPermissionRepository
    {
        public MenuPermissionRepository(ApplicationNameContext db)
            : base(db)
        {
        }
    }
}
