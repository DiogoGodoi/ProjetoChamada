using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
    public class TurmasController : Controller
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IEscolasRepository _escolasRepository;  

        public TurmasController(ITurmaRepository _turmaRepository, IEscolasRepository _escolasRepository)
        {
            this._turmaRepository = _turmaRepository;
            this._escolasRepository = _escolasRepository;

		}
        
        [HttpGet]
        public IActionResult ListTurmas()
        {
            var turmas = _turmaRepository.Turmas();
            return View(turmas);
        }

        [HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateTurmas() {

            MdlTurma turma = new MdlTurma();

            ViewModelTurmaEscola _turmaEscola = new ViewModelTurmaEscola
            {
                _turma = turma,
                _escolas = _escolasRepository.Escolas.ToList()
            };

            return View(_turmaEscola);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTurmas(ViewModelTurmaEscola _turmaEscola) {

            if (ModelState.IsValid)
            {
                _turmaRepository.CreateTurma(_turmaEscola._turma);
                return Redirect("/Turmas/ListTurmas");
            }
            else
            {
                ModelState.AddModelError("", "Erro");
                return View(_turmaEscola);
            }
        }
    }
}
