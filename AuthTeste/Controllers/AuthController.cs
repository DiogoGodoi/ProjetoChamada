﻿using AuthTeste.Components;
using AuthTeste.Models;
using AuthTeste.ViewModels;
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
		public async Task<IActionResult> Login(MdlUsuario usuario)
        {

            if(!String.IsNullOrEmpty(usuario.email) && !String.IsNullOrEmpty(usuario.password))
            {
                var userFind = await _userManager.FindByEmailAsync(usuario.email);

                if(userFind != null)
                {
                    var resultado = await _signInManager.PasswordSignInAsync(userFind, usuario.password, false, false);

                    if(resultado.Succeeded) {

                    HttpContext.Session.SetString("UsuarioEmail", userFind.UserName);

                    return Redirect("/Home/Menu");

                    }
                    else
                    {
						ModelState.AddModelError("", "Erro !!");
						return View(usuario);
					}
                }
                else
                {
					ModelState.AddModelError("", "Erro !!");
					return View();
                        
                }
            }

            ModelState.AddModelError("", "Erro !!");
            return View(usuario);
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
