using AspNetCore3Base.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore3Base.RestAPI.Helpers
{
    public class SeedUserAndRoles
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<ApplicationRole> _roleManager;

        public SeedUserAndRoles(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<Task> SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync
        (UserRoles.User).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = UserRoles.User;
                role.Description = "Perform normal operations.";
                IdentityResult roleResult = _roleManager.
                CreateAsync(role).Result;
            }


            if (!_roleManager.RoleExistsAsync
        (UserRoles.Administrator).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = UserRoles.Administrator;
                role.Description = "Perform all the operations.";
                IdentityResult roleResult = _roleManager.
                CreateAsync(role).Result;
            }

            await SeedUser();

            return Task.CompletedTask;
        }

        public async Task<Task> SeedUser()
        {
            var usr = await _userManager.FindByNameAsync
(UserRoles.UserAdmin);

            if (usr == null)
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
                IdentityResult userResult = await _userManager.CreateAsync(user);

                if (userResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Administrator);
                }
            }

            usr = await _userManager.FindByNameAsync
(UserRoles.UserCommom);

            if (usr == null)
            {
                ApplicationUser user = new ApplicationUser();
                user = new ApplicationUser();
                user.UserName = UserRoles.UserCommom;
                user.Name = UserRoles.UserCommom;
                user.Email = UserRoles.EmailCommom;
                user.IsActive = 1;
                user.IsAppUser = 1;
                user.EmailConfirmed = true;
                user.IsDeleted = 0;
                user.Pin = UserRoles.PinAdmin;
                user.EnableAlerts = 0;
                IdentityResult userCommonResult = await _userManager.CreateAsync(user);
                if (userCommonResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }
            return Task.CompletedTask;

        }
    }
}
