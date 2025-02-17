﻿using Microsoft.AspNetCore.Identity;

namespace AuthTeste.Services.UserRoleService
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRoleService(UserManager<IdentityUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            this._userManager = _userManager;
            this._roleManager = _roleManager;
        }
        public void CreateRole()
        {
			if (!_roleManager.RoleExistsAsync("Master").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Master";
				role.NormalizedName = "MASTER";
				IdentityResult result = _roleManager.CreateAsync(role).Result;
			}
			if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
            if (!_roleManager.RoleExistsAsync("Usuario").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Usuario";
                role.NormalizedName = "USUARIO";
                IdentityResult result = _roleManager.CreateAsync(role).Result;
            }
        }
        public void CreateUser()
        {
            if (_userManager.FindByEmailAsync("admin@localhost.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "Admin";
                user.Email = "admin@localhost.com";
                user.NormalizedUserName = "ADMIN";
                user.NormalizedEmail = "ADMIN@LOCALHOST.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Senh@1234").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Master").Wait();
                }
            }
        }
    }
}
