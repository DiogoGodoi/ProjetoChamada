using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
	public class EscolaController : Controller
	{
		private readonly IEscolasRepository _escolasRepository;
		private string caminhoServer { get; set; }

		public EscolaController(IEscolasRepository _escolasRepository, IWebHostEnvironment sistema)
		{
			this._escolasRepository = _escolasRepository;
			caminhoServer = sistema.WebRootPath;
		}

		[HttpGet]
		public IActionResult ListEscolas()
		{
			try
			{
				var escolas = _escolasRepository.GetEscolas();
				ViewBag.CaminhoImg = "/css/images/escolas.png";
				ViewBag.TitleJumbotron = "ESCOLAS";
				ViewBag.Controller = "Escola";
				ViewBag.Action = "CreateEscola";
				ViewBag.Home = "Home";
				ViewBag.Menu = "Menu";

				if (escolas.Count() == 0)
				{

					ViewBag.Mensagem = "Sem dados a exibir";
					return View(escolas);

				}
				else
				{
					return View(escolas);
				}
			}
			catch (Exception ex)
			{
				ViewBag.Mensagem = "Erro interno" + ex.Message;
				return View();

			}

		}

		[HttpGet]
		[Authorize(Roles = "Admin, Master")]
		public IActionResult CreateEscola()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult CreateEscola(MdlEscola escola, IFormFile arquivo)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return View(escola);
				}
				else
				{
					if (arquivo == null)
					{
						_escolasRepository.InsertEscola(escola);
						TempData["Mensagem"] = "Cadastrado com sucesso";
						return Redirect("/Escola/ListEscolas");
					}
					else
					{
						if (arquivo.FileName.Contains(".jpg") || arquivo.FileName.Contains(".png"))
						{
							string caminhoSave = caminhoServer + "\\imagemData\\";
							string nomeArquivo = Guid.NewGuid().ToString() + "_" + arquivo.FileName;

							if (!Directory.Exists(caminhoSave))
							{
								Directory.CreateDirectory(caminhoSave);
							}

							using (var stream = System.IO.File.Create(caminhoSave + nomeArquivo))
							{
								arquivo.CopyTo(stream);

								escola.UrlImage = nomeArquivo;

								_escolasRepository.InsertEscola(escola);
								TempData["Mensagem"] = "Cadastrado com sucesso";
								return Redirect("/Escola/ListEscolas");
							}
						}
						else
						{
							TempData["Mensagem"] = "Somente arquivos com extensões .png e .jpg são permitidas";
							return View(escola);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ViewBag.Mensagem = "Erro interno" + ex.Message;
				return View(escola);
			}
		}

		[HttpGet]
		public IActionResult GetEscolaId(int id)
		{
			try
			{
				var escola = _escolasRepository.GetEscolaId(id);

				if (escola == null)
				{
					ViewBag.Mensagem = "Sem dados";
					return View(escola);
				}

				return View(escola);
			}
			catch (Exception ex)
			{

				ViewBag.Mensagem = "Erro interno" + ex.Message;
				return StatusCode(400, ViewBag.Mensagem);

			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Master")]
		public IActionResult UpdateEscola(int id)
		{
			try
			{
				var escola = _escolasRepository.GetEscolaId(id);

				if(escola != null)
				{
					return View(escola);
				}
				else
				{
					ViewBag.Mensagem = "Sem dados";
					return View(escola);
				}

			}
			catch(Exception ex) {

				ViewBag.Mensagem = "Erro interno" + ex.Message;

				return StatusCode(400, ViewBag.Mensagem);

			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateEscola(MdlEscola escola)
		{
			if (ModelState.IsValid)
			{
				_escolasRepository.UpdateEscola(escola);

				TempData["Mensagem"] = "Sucesso na atualização";

				return Redirect("/Escola/ListEscolas");
			}
			else
			{
				TempData["Mensagem"] = "Erro na atualização";
				return View(escola);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult RemoveEscola(int id)
		{

			var escola = _escolasRepository.GetEscolaId(id);

			var result = _escolasRepository.RemoveEscola(id);

			if (result == true)
			{
				if (escola.UrlImage != null)
				{
					var caminhoImagem = Path.Combine(caminhoServer, "imagemData", escola.UrlImage);

					if (System.IO.File.Exists(caminhoImagem))
					{
						System.IO.File.Delete(caminhoImagem);
					}

				}

				TempData["Mensagem"] = "Deletado com sucesso";

			}
			else
			{
				TempData["Mensagem"] = "Erro interno";
			}

			return Redirect("/Escola/ListEscolas");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
