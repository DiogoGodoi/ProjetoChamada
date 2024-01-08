using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
			var professores = _professorRepository.GetProfessor();
			ViewBag.CaminhoImg = "/css/images/teacher.png";
			ViewBag.TitleJumbotron = "PROFESSORES";

			return View(professores);
		}

		[HttpGet]
		public IActionResult GetProfessoresId(int id)
		{
			var professor = _professorTurmaRepository.GetProfessoresTurmasId(id);

			return View(professor);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateProfessor()
		{
			MdlProfessor professor = new MdlProfessor();

			var turmas = _turmaRepository.GetTurmas();

			ViewModelProfessorTurma professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlTurmaList = turmas
			};

			return View(professorTurma);

		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
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
				return View(professorTurma);
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteProfessor(int id)
		{
			_professorRepository.RemoveProfessor(id);

			return Redirect("/Professor/ListProfessores");
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult AdcionarTurma(int id)
		{
			var professor = _professorRepository.GetProfessorId(id);
			var turmas = _turmaRepository.GetTurmas();

			ViewModelProfessorTurma _professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlTurmaList = turmas
			};

			return View(_professorTurma);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
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
		[Authorize(Roles = "Admin")]
		public IActionResult RemoverTurma(int id)
		{
			var professor = _professorRepository.GetProfessorId(id);
			var turmas = _turmaRepository.GetTurmas();

			ViewModelProfessorTurma _professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlTurmaList = turmas
			};

			return View(_professorTurma);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
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

					_professorTurmaRepository.RemoverTurmaDoProfessor(_professorTurma.Fk_Professor_Id);
				}
				return Redirect("/Professor/ListProfessores");

			}
			else
			{
				ModelState.AddModelError("", "Erro");
				return View(turmaIds);
			}
		}
	}
}
