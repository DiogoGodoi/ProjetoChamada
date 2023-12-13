using AuthTeste.Models;
using BoundarySMTP;
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
						ModelState.AddModelError("", "Usuario invalido");
						return View(user);
					}
                }
                else
                {
					ModelState.AddModelError("", "Usuario inexistente");
					return View();
                        
                }

            }

            ModelState.AddModelError("", "Falha ao realizar a autenticação");
            return View(user);
        }

		[HttpGet]
		public IActionResult ForgotPassword()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(MdlForgotPassword forgotUser)
		{
			if (ModelState.IsValid)
			{
				var findUser = await _userManager.FindByEmailAsync(forgotUser.email);
				    
                if (findUser != null)
				    {
					    var token = await _userManager.GeneratePasswordResetTokenAsync(findUser);
					    var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = forgotUser.id, code = token }, protocol: HttpContext.Request.Scheme);

                        SmtpConfig smtpConfig = new SmtpConfig();
                        smtpConfig.corpo = $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>.";

					    smtpConfig.EnviarEmail();

					    return Redirect("/Auth/Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Houve um erro durante o processo de recuperação de senha");
                        return View(forgotUser);
                    }
            }
            else
            {
                ModelState.AddModelError("", "Erro interno");
				return View(forgotUser);
            }
		}

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(MdlResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                var userFind = await _userManager.FindByEmailAsync(resetPassword.email);

                if (userFind != null)
                {
                    var resultado = await _userManager.ResetPasswordAsync(userFind, resetPassword.token, resetPassword.password);

                    if (resultado.Succeeded)
                    {
                        return Redirect("/Auth/Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro na redefinição");
                        return View(resetPassword);
                    }
                }
                else
                {
					ModelState.AddModelError("", "Erro interno");
					return View(resetPassword);
				}
            }
            else
            {
				ModelState.AddModelError("", "Erro externo");
				return View();
            }

		}
	}
}
