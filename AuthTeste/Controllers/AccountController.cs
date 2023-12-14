using AuthTeste.Models;
using BoundarySMTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly SmtpConfig _smtpConfig;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, SmtpConfig _smtpConfig)
        {
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._smtpConfig = _smtpConfig;
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            return View();  
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

                    _smtpConfig.EnviarEmail(forgotUser.email);

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
