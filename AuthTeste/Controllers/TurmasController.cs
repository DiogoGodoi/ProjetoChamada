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
            var turmas = _turmaRepository.GetTurmas();
			ViewBag.CaminhoImg = "/css/images/turmas.png";
			ViewBag.TitleJumbotron = "TURMAS";
			ViewBag.Controller = "Turmas";
			ViewBag.Action = "CreateTurmas";
			ViewBag.Home = "Home";
			ViewBag.Menu = "Menu";
			return View(turmas);
        }

        [HttpGet]
        public IActionResult GetTurmasId(int id)
        {
            var turmaProfessores = _professorTurmaRepository.GetTurmaProfessoresId(id);

            return View(turmaProfessores);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Master")]
        public IActionResult CreateTurmas() {

            MdlTurma turma = new MdlTurma();

            ViewBag.Escolas = new SelectList(_escolasRepository.GetEscolas(), "Id", "Nome");

            return View(turma);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Master")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTurmas(MdlTurma turma) {

            if (ModelState.IsValid)
            {
                _turmaRepository.CreateTurma(turma);
                return Redirect("/Turmas/ListTurmas");
            }
            else
            {
                ModelState.AddModelError("", "Erro");
                return View(turma);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Master")]
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
        [Authorize(Roles = "Admin, Master")]
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
        [Authorize(Roles = "Admin, Master")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTurmas(int id)
        {
            _turmaRepository.DeleteTurma(id);

            return Redirect("/Turmas/ListTurmas");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
