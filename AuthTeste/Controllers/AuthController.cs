﻿using AuthTeste.Models;
using BoundarySMTP;
using Microsoft.AspNetCore.Authorization;
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
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(MdlUser user)
        {

            if(ModelState.IsValid)
            {
                var usuario = await _userManager.FindByNameAsync(user.email);

                if(usuario != null)
                {
                    var resultado = await _signInManager.PasswordSignInAsync(usuario, user.password, false, false);

                    if(resultado.Succeeded) {

                    return Redirect("/Home/Index");

                    }
                    else
                    {
						ModelState.AddModelError("", "Erro na autenticacao");
						return View(user);
					}
                }
                else
                {
					ModelState.AddModelError("", "Erro na autenticação");
					return View();
                        
                }

            }

            ModelState.AddModelError("", "Erro na autenticação");
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
