using AuthTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
    public class TurmasController : Controller
    {
        private readonly ITurmaRepository _turmaRepository;

        public TurmasController(ITurmaRepository _turmaRepository)
        {
            this._turmaRepository = _turmaRepository;
        }
        
        [HttpGet]
        public IActionResult ListTurmas()
        {
            var turmas = _turmaRepository.Turmas();
            return View(turmas);
        }
    }
}
