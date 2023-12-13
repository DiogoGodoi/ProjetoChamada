using AuthTeste.Models;
using BoundarySMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthTeste.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private string tokenReset { get; set; } = "";

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

		[HttpGet]
		public IActionResult ForgotPassword()
        {
            return View();
        }

		[HttpPost]
        [AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(MdlForgotPassword forgotUser)
		{

			if (ModelState.IsValid)
			{
				var findUser = await _userManager.FindByNameAsync(forgotUser.email);

				if (findUser != null)
				    {
					    string code = await _userManager.GeneratePasswordResetTokenAsync(findUser);
					    var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = forgotUser.id }, protocol: HttpContext.Request.Scheme);

                        SmtpConfig smtpConfig = new SmtpConfig();
                        smtpConfig.corpo = $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>";

					    smtpConfig.EnviarEmail(forgotUser.email);

                        TempData["TokenGerado"] = code;
					    TempData["Email"] = forgotUser.email;
					    ViewBag.Sucesso = "Link enviado";

					    return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro na recuperação da senha");
                        return View(forgotUser);
                    }
            }
            else
            {
                ModelState.AddModelError("", "Erro na recuperação da senha");
				return View(forgotUser);
            }
		}

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(MdlResetPassword resetPassword)
        {

			if (ModelState.IsValid)
            {
                var userFind = await _userManager.FindByEmailAsync(resetPassword.email);

                if (userFind != null)
                {

                    if(resetPassword.password == resetPassword.confirmPassword)
                    {
                        var resultado = await _userManager.ResetPasswordAsync(userFind, resetPassword.token, resetPassword.confirmPassword);

                        if (resultado.Succeeded)
                        {

                            await _signInManager.RefreshSignInAsync(userFind);

                            ViewBag.Sucesso = "Alterado com sucesso";
						    return View();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Erro na redefinição");
                            return View(resetPassword);
                        }
                    }
                    else
                    {
						ModelState.AddModelError("", "Erro na redefinição");
						return View(resetPassword);
					}
                }
                else
                {
					ModelState.AddModelError("", "Erro na redefinição");
					return View(resetPassword);
				}
            }
            else
            {
				ModelState.AddModelError("", "Erro na redefinição");
				return View(resetPassword);
            }

		}
	}
}
