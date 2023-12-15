using AuthTeste.Models;
using BoundarySMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace AuthTeste.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ISmtpConfig _smtpConfig;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, ISmtpConfig _smtpConfig)
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._smtpConfig = _smtpConfig;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
		public IActionResult CreateAccount()
        {
            return View();  
        }

		[HttpPost]
        [AutoValidateAntiforgeryToken]
		public async Task<IActionResult> CreateAccount(MdlUserCreate usuario)
		{
            if(ModelState.IsValid)
            {

                var user = new IdentityUser { UserName = usuario.email, Email = usuario.email};

                var resultado = await _userManager.CreateAsync(user, usuario.password);


			    if (resultado.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, usuario.permissao);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));   
                    var callBack = Url.Action("ConfirmEmail", "Account", new {User = user.Id, code}, Request.Scheme, Request.Host.ToString());

                    var assunto = _smtpConfig.assunto = "Confirmação de e-mail";
                    _smtpConfig.corpo = $"Por favor, confirme seu e-mail clicando <a href={callBack}>aqui</a>";

                    await _smtpConfig.EnviarEmail(usuario.email, assunto);

					ViewBag.Mensagem = "Link enviado com sucesso";

					return View();

                }
                else
                {
					ModelState.AddModelError("", "Erro 1");
					return View(usuario);
                }

            }
            else
            {
                ModelState.AddModelError("", "Erro 2");
                return View(usuario);
            }

		}

		[AllowAnonymous]
		public async Task<IActionResult> ConfirmEmail(string userId, string code)
		{

			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
			{
				// Trate o erro, por exemplo, redirecionando para uma página de erro
				return RedirectToAction("Login", "Auth");
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);

			if (result.Succeeded)
			{
				// Redirecione para uma página indicando que o e-mail foi confirmado com sucesso
				return RedirectToAction("ConfirmEmail", "Account");
			}
			else
			{
				// Trate o erro, por exemplo, redirecionando para uma página de erro
				return RedirectToAction("Login", "Auth");
			}
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
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = forgotUser.id }, protocol: HttpContext.Request.Scheme);

                    _smtpConfig.corpo = $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>";
                    var assunto = _smtpConfig.assunto = "Redefinição de senha";

                    await _smtpConfig.EnviarEmail(forgotUser.email, assunto);

                    
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

                    if (resetPassword.password == resetPassword.confirmPassword)
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
