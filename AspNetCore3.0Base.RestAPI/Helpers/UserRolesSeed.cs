using AspNetCore3._0Base.Data.Contract;
using AspNetCore3._0Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AspNetCore3._0Base.RestAPI.Helpers
{
    public static class UserRolesSeed
    {
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<ApplicationUser> _userManager;

        //public UserRolesSeed(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        //{
        //    _roleManager = roleManager;
        //    _userManager = userManager;
        //}


        public static void SeedRoles
(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
        ("NormalUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "NormalUser";
               // role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
        ("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                //role.Description = "Perform all the operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
        //public async void Seed()
        //{
        //    if ((await _roleManager.FindByNameAsync(UserRoles.Administrator)) == null)
        //        await _roleManager.CreateAsync(new IdentityRole { Name = UserRoles.Administrator });

        //    if ((await _roleManager.FindByNameAsync(UserRoles.User)) == null)
        //        await _roleManager.CreateAsync(new IdentityRole { Name = UserRoles.User });

        //    await SeedAdmin();
        //    await SeedUser();
        //}

        //public async Task SeedAdmin()
        //{
        //    if (await _userManager.FindByNameAsync(UserRoles.EmailAdmin) == null)
        //    {
        //        ApplicationUser user = new ApplicationUser();
        //        user.UserName = UserRoles.UserAdmin;
        //        user.Name = "IT Administrator";
        //        user.Email = UserRoles.EmailAdmin;
        //        user.IsActive = 1;
        //        user.IsAppUser = 1;
        //        user.EmailConfirmed = true;
        //        user.IsDeleted = 0;
        //        user.Pin = UserRoles.PinAdmin;
        //        user.EnableAlerts = 0;

        //        IdentityResult result = await _userManager.CreateAsync(user, UserRoles.PasswordAdmin);

        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(user, UserRoles.Administrator);
        //        }
        //    }
        //}

        //public async Task SeedUser()
        //{
        //    if (await _userManager.FindByNameAsync(UserRoles.EmailCommom) == null)
        //    {
        //        ApplicationUser user = new ApplicationUser();
        //        user.UserName = UserRoles.UserCommom;
        //        user.Name = UserRoles.UserCommom;
        //        user.Email = UserRoles.EmailCommom;
        //        user.IsActive = 1;
        //        user.IsAppUser = 1;
        //        user.EmailConfirmed = true;
        //        user.IsDeleted = 0;
        //        user.Pin = UserRoles.PinAdmin;
        //        user.EnableAlerts = 0;

        //        IdentityResult result = await _userManager.CreateAsync(user, UserRoles.PasswordUser);

        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(user, UserRoles.User);
        //                       }
        //    }

        //}

    }
}
