using AuthTeste.Models;
using AuthTeste.ViewModels;
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
        private readonly ISmtpConfig _smtpConfig;

        public AccountController(UserManager<IdentityUser> _userManager, 
            SignInManager<IdentityUser> _signInManager, ISmtpConfig _smtpConfig)
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
		public async Task<IActionResult> CreateAccount(ViewModelUsuarios usuario)
		{
            if(ModelState.IsValid)
            {

                if(usuario.mdlUserCreate?.password == usuario.mdlUserCreate?.confirmPassword)
                {

                    var user = new IdentityUser { UserName = usuario.mdlUserCreate?.userName, Email = usuario.mdlUserCreate?.email };

                    var resultado = await _userManager.CreateAsync(user, usuario.mdlUserCreate.password);


                    if (resultado.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, usuario.mdlUserCreate.permissao);

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callBack = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

                        var assunto = _smtpConfig.assunto = "Confirmação de e-mail";
                        _smtpConfig.corpo = $"Por favor, confirme seu e-mail clicando <a href=\"{callBack}\">aqui</a>";

                        await _smtpConfig.EnviarEmail(usuario.mdlUserCreate.email, assunto);

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
                    ModelState.AddModelError("", "As senhas não coicidem");
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

			var usuario = await _userManager.FindByIdAsync(userId);

            if (usuario == null)
            {
                //Trate o erro, por exemplo, redirecionando para uma página de erro

                return RedirectToAction("Login", "Auth");
            }
            else
            {
                var result = await _userManager.ConfirmEmailAsync(usuario, code);

                if (result.Succeeded == true)
                {
                    ViewBag.ConfirmationStatus = true;
                    return View();
                }
                else
                {
                    ViewBag.ConfirmationStatus = false;
                    return RedirectToAction("Login", "Auth");
                }
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
        public async Task<IActionResult> ForgotPassword(ViewModelUsuarios forgotUser)
        {

            if (ModelState.IsValid)
            {
                var findUser = await _userManager.FindByEmailAsync(forgotUser.mdlForgotPassword.email);

                if (findUser != null)
                {
                    string code = await _userManager.GeneratePasswordResetTokenAsync(findUser);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = forgotUser.mdlForgotPassword.id }, protocol: HttpContext.Request.Scheme);

                    _smtpConfig.corpo = $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>";
                    var assunto = _smtpConfig.assunto = "Redefinição de senha";

                    await _smtpConfig.EnviarEmail(forgotUser.mdlForgotPassword.email, assunto);

                    
                    TempData["TokenGerado"] = code;
                    TempData["Email"] = forgotUser.mdlForgotPassword.email;
                    ViewBag.Sucesso = "Link enviado";

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Erro 1");
                    return View(forgotUser);
                }
            }
            else
            {
                ModelState.AddModelError("", "Erro 2");
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
        public async Task<IActionResult> ResetPassword(ViewModelUsuarios resetPassword)
        {

            if (ModelState.IsValid)
            {
                var userFind = await _userManager.FindByEmailAsync(resetPassword.mdlResetPassword.email);

                if (userFind != null)
                {

                    if (resetPassword.mdlResetPassword.password == resetPassword.mdlResetPassword.confirmPassword)
                    {
                        var resultado = await _userManager.ResetPasswordAsync(userFind, resetPassword.mdlResetPassword.token, resetPassword.mdlResetPassword.confirmPassword);

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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListAccount()
        {

            var usuarios = _userManager.Users.ToList();

            List<MdlUsuariosRoles> usuariosRolesList = new List<MdlUsuariosRoles>();

            foreach (var idx in usuarios)
            {
                var roles = _userManager.GetRolesAsync(idx).Result;

                MdlUsuariosRoles usuariosRoles = new MdlUsuariosRoles
                {
                    Id = idx.Id,
                    UserName = idx.UserName,
                    Email = idx.Email,
                    EmailConfirmed = idx.EmailConfirmed,
                    Roles = roles
                };

                usuariosRolesList.Add(usuariosRoles);   
            }


            return View(usuariosRolesList);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAccountId(string id)
        {
            var usuario = _userManager.Users.FirstOrDefault(i => i.Id == id);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário inexistente");
                return View();
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAccount(string id)
        {
            var usuario = _userManager.Users.FirstOrDefault(i => i.Id == id);

            if(usuario == null)
            {
                ModelState.AddModelError("", "Usuário inexistente");
                return View();
            }
            else
            {
                var result = _userManager.DeleteAsync(usuario).Result;

                if(result.Succeeded)
                {
                    return Redirect("/Account/ListAccount");
                }
                else
                {
                    ModelState.AddModelError("", "Erro interno");
                    return View();
                }
            }

        }
    }
  
}
