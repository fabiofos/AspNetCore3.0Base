using AspNetCore3Base.Application.Interface;
using AspNetCore3Base.Domain.Entities;
using AspNetCore3Base.Domain.Interfaces.Services;


namespace AspNetCore3Base.Application.Services
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
