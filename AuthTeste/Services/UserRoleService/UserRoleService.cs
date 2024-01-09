using Microsoft.AspNetCore.Identity;

namespace AuthTeste.Services.UserRoleService
{
	public class UserRoleService: IUserRoleService
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public void CreateRole()
		{
			if (!_roleManager.RoleExistsAsync("Admin").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "Admin";
				role.NormalizedName = "ADMIN";
				IdentityResult result = _roleManager.CreateAsync(role).Result;
			}
			if (!_roleManager.RoleExistsAsync("User").Result)
			{
				IdentityRole role = new IdentityRole();
				role.Name = "User";
				role.NormalizedName = "USER";
				IdentityResult result = _roleManager.CreateAsync(role).Result;
			}
		}
		public void CreateUser()
		{

			if (_userManager.FindByEmailAsync("admin@localhost.com").Result == null)
			{
				IdentityUser user = new IdentityUser();
				user.UserName = "Administrador";
				user.Email = "admin@localhost.com";
				user.NormalizedUserName = "ADMINISTRADOR";
				user.NormalizedEmail = "ADMIN@LOCALHOST.COM";
				user.EmailConfirmed = true;
				user.LockoutEnabled = false;
				user.SecurityStamp = Guid.NewGuid().ToString();

				IdentityResult result = _userManager.CreateAsync(user, "Senh@1234").Result;

				if (result.Succeeded)
				{
					_userManager.AddToRoleAsync(user, "Admin").Wait();
				}
			}


		}

	}
}
