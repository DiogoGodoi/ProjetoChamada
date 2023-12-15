using AuthTeste.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(MdlUserAuth user)
        {

            if(ModelState.IsValid)
            {
                var usuario = await _userManager.FindByNameAsync(user.email);

                if(usuario != null)
                {
                    var resultado = await _signInManager.PasswordSignInAsync(usuario, user.password, false, false);

                    if(resultado.Succeeded) {

                    return Redirect("/Home/Menu");

                    }
                    else
                    {
						ModelState.AddModelError("", "Erro !!");
						return View(user);
					}
                }
                else
                {
					ModelState.AddModelError("", "Erro !!");
					return View();
                        
                }

            }

            ModelState.AddModelError("", "Erro !!");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
	}
}
