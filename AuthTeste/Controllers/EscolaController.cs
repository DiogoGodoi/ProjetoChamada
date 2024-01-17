using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.Services.UploadFileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
	public class EscolaController : Controller
	{
		private readonly IEscolasRepository _escolasRepository;
		private readonly IFileManager _fileManager;
		private string caminhoServer { get; set; }

		public EscolaController(IEscolasRepository _escolasRepository, IWebHostEnvironment sistema, IFileManager _fileManager)
		{
			this._escolasRepository = _escolasRepository;
			this._fileManager = _fileManager;
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
					var caminho = "\\imagens\\escola\\";
					var retornoServiceUpload = _fileManager.CreateFile(escola, arquivo, caminho);

					if (retornoServiceUpload == 1)
					{
						_escolasRepository.InsertEscola(escola);
						TempData["Mensagem"] = "Cadastrado com sucesso";
						return Redirect("/Escola/ListEscolas");
					}
					else if (retornoServiceUpload == 2)
					{
						TempData["Mensagem"] = "Somente arquivos com extensões .png e .jpg são permitidas";
						return View(escola);
					}
					else if (retornoServiceUpload == 3)
					{
						_escolasRepository.InsertEscola(escola);
						TempData["Mensagem"] = "Cadastrado com sucesso";
						return Redirect("/Escola/ListEscolas");
					}
					else
					{
						return StatusCode(400);
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

				ViewBag.Controller = "Escola";
				ViewBag.Action = "RemoveEscola";
				ViewBag.RouteId = escola.Id;

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

				if (escola != null)
				{
					return View(escola);
				}
				else
				{
					ViewBag.Mensagem = "Sem dados";
					return View(escola);
				}

			}
			catch (Exception ex)
			{

				ViewBag.Mensagem = "Erro interno" + ex.Message;

				return StatusCode(400, ViewBag.Mensagem);

			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateEscola(MdlEscola escola, IFormFile arquivo)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var escolaId = _escolasRepository.GetEscolaId(escola.Id);

					if (arquivo == null)
					{
						_escolasRepository.UpdateEscola(escola);
						TempData["Mensagem"] = "Atualizado com sucesso";
						return Redirect("/Escola/ListEscolas");
					}
					else
					{
						if (!String.IsNullOrEmpty(escolaId.UrlImage))
						{
							if (arquivo.FileName.Contains(".jpg") || arquivo.FileName.Contains(".png") || arquivo.FileName.Contains(".jpeg"))
							{
								var caminhoUrl = "\\imagens\\escola\\";
								var retornoRemove = _fileManager.DeleteFile(escolaId, caminhoUrl);

								if (retornoRemove == true)
								{
									var retornoCreate = _fileManager.CreateFile(escola, arquivo, caminhoUrl);

									if (retornoCreate == 1)
									{
										_escolasRepository.UpdateEscola(escola);
										TempData["Mensagem"] = "Atualizado com sucesso";
										return Redirect("/Escola/ListEscolas");
									}
									else if (retornoCreate == 2)
									{
										TempData["Mensagem"] = "Somente arquivos com extensões .png e .jpg são permitidas";
										return View(escola);
									}
									else if (retornoCreate == 3)
									{
										_escolasRepository.UpdateEscola(escola);
										TempData["Mensagem"] = "Atualizado com sucesso";
										return Redirect("/Escola/ListEscolas");
									}
									else
									{
										return StatusCode(400);
									}
								}
								else
								{
									TempData["Mensagem"] = "Erro interno 1";
									return Redirect("/Escola/ListEscolas");
								}
							}
							else
							{
								TempData["Mensagem"] = "Somente arquivos com extensões .png e .jpg são permitidas";
								return View(escola);
							}
						}
						else
						{
							TempData["Mensagem"] = "Erro interno 2";
							return Redirect("/Escola/ListEscolas");
						}
					}
				}
				else
				{
					return View(escola);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro " + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult RemoveEscola(int id)
		{
			try
			{
				var escola = _escolasRepository.GetEscolaId(id);
				var result = _escolasRepository.RemoveEscola(id);

				if (result == true)
				{
					var retorno = _fileManager.DeleteFile(escola, "\\imagens\\escola\\");

					if (retorno == true)
					{
						TempData["Mensagem"] = "Dados deletados com sucesso";
						return Redirect("/Escola/ListEscolas");
					}
					else
					{
						TempData["Mensagem"] = "Registro deletado com sucesso";
						return Redirect("/Escola/ListEscolas");
					}
				}
				else
				{
					TempData["Mensagem"] = "Erro na remoção";
					return Redirect("/Escola/ListEscolas");
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
