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
            var turmas = _turmaRepository.GetTurmas();
            return View(turmas);
        }

        [HttpGet]
        public IActionResult GetTurmasId(int id)
        {
            var turma = _turmaRepository.GetById(id);

            return View(turma);
        }

        [HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult CreateTurmas() {

            MdlTurma turma = new MdlTurma();

            ViewModelTurmaEscola _turmaEscola = new ViewModelTurmaEscola
            {
                _turma = turma,
                _escolas = _escolasRepository.GetEscolas()
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateTurmas(int id)
        {
            var turma =  _turmaRepository.GetById(id);

		    ViewModelTurmaEscola _turmaEscola = new ViewModelTurmaEscola
			{
				_turma = turma,
				_escolas = _escolasRepository.GetEscolas()
			};

			return View(_turmaEscola);
        }

        [HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
        public IActionResult UpdateTurmas(ViewModelTurmaEscola _turmaEscola)
        {
            if (ModelState.IsValid)
            {
                if(_turmaEscola._turma == null)
                {
					ModelState.AddModelError("", "Erro");
					return View(_turmaEscola._turma);
				}
                _turmaRepository.UpdateTurma(_turmaEscola._turma);
                return Redirect("/Turmas/ListTurmas");
            }
            else
            {
                ModelState.AddModelError("", "Erro");
                return View(_turmaEscola._turma);
            }
        }

		[HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTurmas(int id)
        {
            _turmaRepository.DeleteTurma(id);

            return Redirect("/Turmas/ListTurmas");
        }
    }
}
