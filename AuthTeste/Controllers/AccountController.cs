using AuthTeste.Models;
using AuthTeste.Repository;
using AuthTeste.Services.EmailService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
    public class AccountController : Controller
	{

		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IEmailService _smtpConfig;

		public AccountController(UserManager<IdentityUser> _userManager,
			SignInManager<IdentityUser> _signInManager, IEmailService _smtpConfig)
		{
			this._userManager = _userManager;
			this._signInManager = _signInManager;
			this._smtpConfig = _smtpConfig;
		}

		[HttpGet]
		[Authorize(Roles = "Master")]
		public IActionResult CreateAccount()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Master")]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> CreateAccount(MdlUsuario usuario)
		{
			if (ModelState.IsValid)
			{

				if (usuario.password == usuario.confirmPassword)
				{

					var user = new IdentityUser {
						UserName = usuario.userName,
						Email = usuario.email,
						NormalizedUserName = usuario.userName.ToUpper(),
						NormalizedEmail = usuario.email.ToUpper(),
						LockoutEnabled = false,
						SecurityStamp = Guid.NewGuid().ToString()

					};

					IdentityResult resultado = await _userManager.CreateAsync(user, usuario.password);


					if (resultado.Succeeded)
					{

						await _userManager.AddToRoleAsync(user, usuario.permissao);

						var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
						var callBack = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);

						var assunto = _smtpConfig.assunto = "Confirmação de e-mail";
						_smtpConfig.corpo = $"Por favor, confirme seu e-mail clicando <a href=\"{callBack}\">aqui</a>";

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
				return RedirectToAction("Login", "Auth");
			}
			else
			{
				IdentityResult result = await _userManager.ConfirmEmailAsync(usuario, code);

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
		public async Task<IActionResult> ForgotPassword(MdlUsuario usuario)
		{

			if (!String.IsNullOrEmpty(usuario.email))
			{
				var findUser = await _userManager.FindByEmailAsync(usuario.email);

				if (findUser != null)
				{
					string code = await _userManager.GeneratePasswordResetTokenAsync(findUser);
					var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = usuario.id }, protocol: HttpContext.Request.Scheme);

					_smtpConfig.corpo = $"Por favor, redefina sua senha clicando <a href='{callbackUrl}'>aqui</a>";
					var assunto = _smtpConfig.assunto = "Redefinição de senha";

					await _smtpConfig.EnviarEmail(usuario.email, assunto);


					TempData["TokenGerado"] = code;
					TempData["Email"] = usuario.email;
					ViewBag.Sucesso = "Link enviado";

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

		[HttpGet]
		public IActionResult ResetPassword()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(MdlUsuario usuario)
		{

			if (!String.IsNullOrEmpty(usuario.email) 
				&& !String.IsNullOrEmpty(usuario.password) 
				&& !String.IsNullOrEmpty(usuario.confirmPassword))
			{
				var userFind = await _userManager.FindByEmailAsync(usuario.email);

				if (userFind != null)
				{
					if (usuario.password == usuario.confirmPassword)
					{
						var resultado = await _userManager.ResetPasswordAsync(userFind, usuario.token, usuario.confirmPassword);

						if (resultado.Succeeded)
						{

							await _signInManager.RefreshSignInAsync(userFind);
							await _signInManager.SignOutAsync();

							ViewBag.Sucesso = "Alterado com sucesso";
							return View();
						}
						else
						{
							ModelState.AddModelError("", "Erro na redefinição");
							return View(usuario);
						}
					}
					else
					{
						ModelState.AddModelError("", "Erro na redefinição");
						return View(usuario);
					}
				}
				else
				{
					ModelState.AddModelError("", "Erro na redefinição");
					return View(usuario);
				}
			}
			else
			{
				ModelState.AddModelError("", "Erro na redefinição");
				return View(usuario);
			}

		}

		[HttpGet]
		[Authorize(Roles = "Master")]
		public IActionResult ListAccount()
		{

			var usuarios = _userManager.Users.ToList();
			ViewBag.CaminhoImg = "/css/images/userIcon.png";
			ViewBag.TitleJumbotron = "USUÁRIOS";

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
		[Authorize(Roles = "Master")]
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
		[Authorize(Roles = "Master")]
		public IActionResult DeleteAccount(string id)
		{
			var usuario = _userManager.Users.FirstOrDefault(i => i.Id == id);

			if (_userManager.Users.Count() == 1)
			{
				ModelState.AddModelError("", "Você não pode excluir todos os usuários");
				return View(usuario);
			}
			else
			{
				if (usuario == null)
				{
					ModelState.AddModelError("", "Usuário inexistente");
					return View();
				}
				else
				{

					if (_userManager.IsInRoleAsync(usuario, "Master").Result)
					{
						ModelState.AddModelError("", "O usuário master não pode ser excluido");
						return View();
					}

					else
					{
						var result = _userManager.DeleteAsync(usuario).Result;

						if (result.Succeeded)
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

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
