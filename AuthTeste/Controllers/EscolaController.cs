using AuthTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Controllers
{
	public class EscolaController : Controller
	{
		private readonly IEscolasRepository _escolasRepository;

		public EscolaController(IEscolasRepository _escolasRepository)
		{
			this._escolasRepository = _escolasRepository;
		}

		public IActionResult ListEscolas()
		{
			var escolas = _escolasRepository.Escolas.ToList();

			if(escolas.Count == 0)
			{

				ModelState.AddModelError("", "Sem dados a exibir");
				return View();

			}
			else
			{
				return View(escolas);
			}

		}
	}
}
