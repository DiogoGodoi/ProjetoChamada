using AuthTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
	public class ProfessorController : Controller
	{
		private readonly IProfessorRepository _professorRepository;

		public ProfessorController(IProfessorRepository _professorRepository) { 

			this._professorRepository = _professorRepository;
		}

		[HttpGet]
		public IActionResult ListProfessores()
		{
			var professores = _professorRepository.GetProfessores();

			return View(professores);
		}
	}
}
