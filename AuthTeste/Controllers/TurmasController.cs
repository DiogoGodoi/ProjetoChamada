using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthTeste.Controllers
{
	public class TurmasController : Controller
	{
		private readonly ITurmaRepository _turmaRepository;
		private readonly IProfessorTurmaRepository _professorTurmaRepository;
		private readonly IEscolasRepository _escolasRepository;
		public TurmasController(ITurmaRepository _turmaRepository, IEscolasRepository _escolasRepository,
			IProfessorTurmaRepository _professorTurmaRepository)
		{
			this._turmaRepository = _turmaRepository;
			this._escolasRepository = _escolasRepository;
			this._professorTurmaRepository = _professorTurmaRepository;

		}

		[HttpGet]
		public IActionResult ListTurmas()
		{
			try
			{
				ViewBag.CaminhoImg = "/css/images/turmas.png";
				ViewBag.TitleJumbotron = "TURMAS";
				ViewBag.Controller = "Turmas";
				ViewBag.Action = "CreateTurmas";
				ViewBag.Home = "Home";
				ViewBag.Menu = "Menu";

				var turmas = _turmaRepository.GetTurmas();

				return View(turmas);

			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}

		[HttpGet]
		public IActionResult GetTurmasId(int id)
		{
			try
			{
				var turmaProfessores = _professorTurmaRepository.GetTurmaProfessoresId(id);
				ViewBag.Controller = "Turmas";
				ViewBag.Action = "DeleteTurmas";
				ViewBag.RouteId = turmaProfessores._mdlTurma.Id;

				if (turmaProfessores != null)
				{
					return View(turmaProfessores);
				}
				else
				{
					return StatusCode(404);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Master")]
		public IActionResult CreateTurmas()
		{
			try
			{
				ViewBag.Escolas = new SelectList(_escolasRepository.GetEscolas(), "Id", "Nome");

				return View();
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult CreateTurmas(MdlTurma turma)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_turmaRepository.CreateTurma(turma);
					TempData["Mensagem"] = "Cadastrado com sucesso";
					return Redirect("/Turmas/ListTurmas");
				}
				else
				{
					TempData["Mensagem"] = "Erro no cadastro";
					return View(turma);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}

		[HttpGet]
		[Authorize(Roles = "Admin, Master")]
		public IActionResult UpdateTurmas(int id)
		{
			try
			{
				var turma = _turmaRepository.GetById(id);

				if (turma != null)
				{
					ViewModelTurmaEscola _turmaEscola = new ViewModelTurmaEscola
					{
						_turma = turma,
						_escolas = _escolasRepository.GetEscolas()
					};

					return View(_turmaEscola);
				}
				else
				{
					return StatusCode(404);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateTurmas(ViewModelTurmaEscola _turmaEscola)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (_turmaEscola._turma == null)
					{
						TempData["Mensagem"] = "Erro na alteração";
						return View(_turmaEscola._turma);
					}
					_turmaRepository.UpdateTurma(_turmaEscola._turma);
					TempData["Mensagem"] = "Alteração realizada com sucesso";
					return Redirect("/Turmas/ListTurmas");
				}
				else
				{
					TempData["Mensagem"] = "Erro interno";
					return View(_turmaEscola._turma);
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteTurmas(int id)
		{
			try
			{
				_turmaRepository.DeleteTurma(id);
				TempData["Mensagem"] = "Removido com sucesso";
				return Redirect("/Turmas/ListTurmas");
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
