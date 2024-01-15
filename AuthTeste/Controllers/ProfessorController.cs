using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthTeste.Controllers
{
	public class ProfessorController : Controller
	{
		private readonly IProfessorRepository _professorRepository;
		private readonly IProfessorTurmaRepository _professorTurmaRepository;
		private readonly ITurmaRepository _turmaRepository;

		public ProfessorController(IProfessorRepository _professorRepository,
			IProfessorTurmaRepository _professorTurmaRepository, ITurmaRepository _turmaRepository)
		{

			this._professorRepository = _professorRepository;
			this._professorTurmaRepository = _professorTurmaRepository;
			this._turmaRepository = _turmaRepository;
		}

		[HttpGet]
		public IActionResult ListProfessores()
		{
			try
			{
				ViewBag.CaminhoImg = "/css/images/teacher.png";
				ViewBag.TitleJumbotron = "PROFESSORES";
				ViewBag.Controller = "Professor";
				ViewBag.Action = "CreateProfessor";
				ViewBag.Home = "Home";
				ViewBag.Menu = "Menu";
				var professores = _professorRepository.GetProfessor();

				if (professores != null)
				{
					return View(professores);
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
		public IActionResult GetProfessoresId(int id)
		{
			try
			{
				var professor = _professorTurmaRepository.GetProfessoresTurmasId(id);

				ViewBag.Controller = "Professor";
				ViewBag.Action = "DeleteProfessor";
				ViewBag.RouteId = professor._mdlProfessor.Id;

				if (professor != null)
				{
					return View(professor);
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
		public IActionResult CreateProfessor()
		{
	
			ViewBag.Turmas = new MultiSelectList(_turmaRepository.GetTurmas(), "Id", "Nome");

			return View();

		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult CreateProfessor(ViewModelProfessorTurma professorTurma, List<int> turmaIds)
		{

			if (ModelState.IsValid)
			{
				_professorRepository.CreateProfessor(professorTurma._mdlProfessor);

				foreach (var idx in turmaIds)
				{
					MdlProfessorTurma _professorTurma = new MdlProfessorTurma
					{
						Fk_Professor_Id = professorTurma._mdlProfessor.Id,
						Fk_Turma_Id = idx
					};

					_professorTurmaRepository.CreateProfessorTurma(_professorTurma);
				}

				return Redirect("/Professor/ListProfessores");

			}
			else
			{
				ModelState.AddModelError("", "Erro");
				return View();
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteProfessor(int id)
		{
			_professorRepository.RemoveProfessor(id);

			return Redirect("/Professor/ListProfessores");
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Master")]
		public IActionResult AdcionarTurma(int id)
		{
			var professor = _professorRepository.GetProfessorId(id);
			var turmas = _turmaRepository.GetTurmas();

			ViewModelProfessorTurma _professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlTurmaList = turmas
			};

			ViewBag.Turmas = new MultiSelectList(_turmaRepository.GetTurmas(), "Id", "Nome");
			return View(_professorTurma);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult AdcionarTurma(int id, ViewModelProfessorTurma professorTurma, List<int> turmaIds)
		{
			if (ModelState.IsValid)
			{
				_professorRepository.UpdateProfessor(professorTurma._mdlProfessor);
				professorTurma._mdlProfessor.Id = id;

				foreach (var idx in turmaIds)
				{
					MdlProfessorTurma _professorTurma = new MdlProfessorTurma
					{
						Fk_Professor_Id = professorTurma._mdlProfessor.Id,
						Fk_Turma_Id = idx
					};

					_professorTurmaRepository.AdcionarTurmaAoProfessor(_professorTurma);

				}


				return Redirect("/Professor/ListProfessores");

			}
			else
			{
				ModelState.AddModelError("", "Erro");
				return View(professorTurma);
			}
		}

		[HttpGet]
		[Authorize(Roles = "Admin, Master")]
		public IActionResult RemoverTurma(int id)
		{
			var professor = _professorRepository.GetProfessorId(id);
			var turmas = _turmaRepository.GetTurmas();

			ViewModelProfessorTurma _professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlTurmaList = turmas
			};

			ViewBag.Turmas = new MultiSelectList(_turmaRepository.GetTurmas(), "Id", "Nome");

			return View(_professorTurma);
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Master")]
		[ValidateAntiForgeryToken]
		public IActionResult RemoverTurma(int id, List<int> turmaIds)
		{
			if (ModelState.IsValid)
			{

				foreach (var idx in turmaIds)
				{
					MdlProfessorTurma _professorTurma = new MdlProfessorTurma
					{
						Fk_Professor_Id = id,
						Fk_Turma_Id = idx

					};

					_professorTurmaRepository.RemoverTurmaDoProfessor(_professorTurma);
				}
				return Redirect("/Professor/ListProfessores");

			}
			else
			{
				ModelState.AddModelError("", "Erro");
				return View(turmaIds);
			}
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
