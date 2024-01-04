using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
	public class ProfessorController : Controller
	{
		private readonly IProfessorRepository _professorRepository;
		private readonly IProfessorTurmaRepository _professorTurmaRepository;
		private readonly ITurmaRepository _turmaRepository;

		public ProfessorController(IProfessorRepository _professorRepository, 
			IProfessorTurmaRepository _professorTurmaRepository, ITurmaRepository _turmaRepository) { 

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
		public IActionResult CreateProfessor(ViewModelProfessorTurma professorTurma, List<int>turmaIds)
		{
			_professorRepository.CreateProfessor(professorTurma._mdlProfessor);

			foreach (var id in turmaIds)
			{
				professorTurma._professorTurma.Fk_Professor_Id = professorTurma._mdlProfessor.Id;
				professorTurma._professorTurma.Fk_Turma_Id = id;
				_professorTurmaRepository.CreateProfessorTurma(professorTurma._professorTurma);
			}	

			return Redirect("/Professor/ListProfessores");
		}
	}
}
