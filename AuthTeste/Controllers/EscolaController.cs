using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
	public class EscolaController : Controller
	{
		private readonly IEscolasRepository _escolasRepository;

		public EscolaController(IEscolasRepository _escolasRepository)
		{
			this._escolasRepository = _escolasRepository;
		}

		[HttpGet]
		public IActionResult ListEscolas()
		{
			var escolas = _escolasRepository.Escolas.ToList();

			if(escolas.Count == 0)
			{

				ModelState.AddModelError("", "Sem dados a exibir");
				return View();

			}
			else
			{
				return View(escolas);
			}

		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateEscola()
		{
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateEscola(MdlEscola escola)
		{
			if(!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Erro");
				return View(escola);
			}
			else
			{
				_escolasRepository.InsertEscola(escola);
				ViewBag.Mensagem = "Cadastrado com sucesso";
				return View();
			}			
		}

		[HttpGet]
		public IActionResult GetEscolaId(int id)
		{
			var escola = _escolasRepository.GetEscolaId(id);

			return View(escola);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult UpdateEscola(int id)
		{
			var escola = _escolasRepository.GetEscolaId(id);

			return View(escola);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateEscola(MdlEscola escola)
		{
			if(ModelState.IsValid)
			{
				_escolasRepository.UpdateEscola(escola);
				return Redirect("/Escola/ListEscolas");
			}
			else
			{
				return View(escola);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult RemoveEscola(int id)
		{
			_escolasRepository.RemoveEscola(id);

			return Redirect("/Escola/ListEscolas");
		}
	}
}
