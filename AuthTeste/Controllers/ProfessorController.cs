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

		public ProfessorController(IProfessorRepository _professorRepository, IProfessorTurmaRepository _professorTurmaRepository) { 

			this._professorRepository = _professorRepository;
			this._professorTurmaRepository = _professorTurmaRepository;
		}

		[HttpGet]
		public IActionResult ListProfessores()
		{
			var professores = _professorRepository.GetProfessor();

			return View(professores);
		}

		[HttpGet]
		public IActionResult GetProfessoresId(int id)
		{
			var professor = _professorRepository.GetProfessorId(id);

			return View(professor);
		}

		[HttpGet]
		public IActionResult CreateProfessor()
		{
			MdlProfessor professor = new MdlProfessor();

			ViewModelProfessorTurma professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlProfessorTurmaList = _professorTurmaRepository.ListProfessoresTurmas()
			};

			return View(professorTurma);

		}
	}
}
