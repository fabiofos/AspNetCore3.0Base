using AspNetCore3._0Base.Data.Contract;
using AspNetCore3._0Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AspNetCore3._0Base.RestAPI.Helpers
{
    public static class UserRolesSeed
    {
        public static void SeedRoles
(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
        (UserRoles.User).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = UserRoles.User;
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
           

            if (!roleManager.RoleExistsAsync
        (UserRoles.Administrator).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = UserRoles.Administrator;
                role.Description = "Perform all the operations.";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }

        public static async void SeedUser
        (UserManager<ApplicationUser> userManager)
        {

            if (await userManager.FindByNameAsync
      (UserRoles.User) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserRoles.UserAdmin;
                user.Name = "IT Administrator";
                user.Email = UserRoles.EmailAdmin;
                user.IsActive = 1;
                user.IsAppUser = 1;
                user.EmailConfirmed = true;
                user.IsDeleted = 0;
                user.Pin = UserRoles.PinAdmin;
                user.EnableAlerts = 0;
                IdentityResult userResult = userManager.
                CreateAsync(user).Result;

                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Administrator);
                }
            }

            if (await userManager.FindByNameAsync
(UserRoles.UserCommom) == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserRoles.UserCommom;
                user.Name = UserRoles.UserCommom;
                user.Email = UserRoles.EmailCommom;
                user.IsActive = 1;
                user.IsAppUser = 1;
                user.EmailConfirmed = true;
                user.IsDeleted = 0;
                user.Pin = UserRoles.PinAdmin;
                user.EnableAlerts = 0;
                IdentityResult userResult = userManager.
                CreateAsync(user).Result;
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }
        }
    }
}
