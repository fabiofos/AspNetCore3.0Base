using AspNetCore3._0Base.Application.Interface;
using AspNetCore3._0Base.Domain.Entities;
using AspNetCore3._0Base.Domain.Interfaces.Services;


namespace AspNetCore3._0Base.Application.Services
{
    public class MenuPermissionAppService : AppServiceBase<MenuPermission>, IMenuPermissionAppService
    {
        private readonly IMenuPermissionService _menuPermissionService;
        public MenuPermissionAppService(IMenuPermissionService menuPermissionService)
            : base(menuPermissionService)
        {
            _menuPermissionService = menuPermissionService;
        }

    }
}
