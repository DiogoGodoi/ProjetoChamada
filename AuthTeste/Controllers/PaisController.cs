using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthTeste.Controllers
{
	public class PaisController : Controller
	{

		private readonly IPaisRepository _paisRepository;

		public PaisController(IPaisRepository _paisRepository)
		{
			this._paisRepository = _paisRepository;
		}

		[HttpGet]
		public IActionResult ListPais()
		{
			var pais = _paisRepository.ListPais();
			ViewBag.CaminhoImg = "/css/images/phone.png";
			ViewBag.TitleJumbotron = "PAIS";
			ViewBag.Controller = "Pais";
			ViewBag.Action = "CreatePais";
			ViewBag.Home = "Home";
			ViewBag.Menu = "Menu";

			return View(pais);
		}

		[HttpGet]
		public IActionResult GetPaisId(int id)
		{
			var pais = _paisRepository.GetPaisId(id);
			return View(pais);
		}

		[HttpGet]
		[Authorize(Roles = "Master, Admin")]
		public IActionResult CreatePais()
		{
			List<SelectListItem> sexos = new List<SelectListItem> {

				new SelectListItem { Value = "M", Text = "Masculino" },
				new SelectListItem { Value = "F", Text = "Feminino" }

			};
			List<SelectListItem> estados = new List<SelectListItem> {

				new SelectListItem { Value = "AC", Text = "Acre" },
				new SelectListItem { Value = "AL", Text = "Alagoas" },
				new SelectListItem { Value = "AP", Text = "Amapá" },
				new SelectListItem { Value = "AM", Text = "Amazonas" },
				new SelectListItem { Value = "BA", Text = "Bahia" },
				new SelectListItem { Value = "CE", Text = "Ceará" },
				new SelectListItem { Value = "DF", Text = "Distrito Federal" },
				new SelectListItem { Value = "ES", Text = "Espírito Santo" },
				new SelectListItem { Value = "GO", Text = "Goiás" },
				new SelectListItem { Value = "MA", Text = "Maranhão" },
				new SelectListItem { Value = "MT", Text = "Mato Grosso" },
				new SelectListItem { Value = "MS", Text = "Mato Grosso do Sul" },
				new SelectListItem { Value = "MG", Text = "Minas Gerais" },
				new SelectListItem { Value = "PA", Text = "Pará" },
				new SelectListItem { Value = "PB", Text = "Paraíba" },
				new SelectListItem { Value = "PR", Text = "Paraná" },
				new SelectListItem { Value = "PE", Text = "Pernambuco" },
				new SelectListItem { Value = "PI", Text = "Piauí" },
				new SelectListItem { Value = "RJ", Text = "Rio de Janeiro" },
				new SelectListItem { Value = "RN", Text = "Rio Grande do Norte" },
				new SelectListItem { Value = "RS", Text = "Rio Grande do Sul" },
				new SelectListItem { Value = "RO", Text = "Rondônia" },
				new SelectListItem { Value = "RR", Text = "Roraima" },
				new SelectListItem { Value = "SC", Text = "Santa Catarina" },
				new SelectListItem { Value = "SP", Text = "São Paulo" },
				new SelectListItem { Value = "SE", Text = "Sergipe" },
				new SelectListItem { Value = "TO", Text = "Tocantins" }
			};

			ViewBag.Estado = new SelectList(estados, "Value", "Text");
			ViewBag.Sexo = new SelectList(sexos, "Value", "Text");

			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Master, Admin")]
		[ValidateAntiForgeryToken]
		public IActionResult CreatePais(MdlPais pais)
		{
			if (ModelState.IsValid)
			{
				_paisRepository.CreatePais(pais);
				return Redirect("/Pais/ListPais");
			}
			else
			{
				ModelState.AddModelError(" ", "Erro");
				return View(pais);
			}

			
		}

		[HttpGet]
		[Authorize(Roles = "Master, Admin")]
		public IActionResult UpdatePais(int id)
		{
			var pais = _paisRepository.GetPaisId(id);

			List<SelectListItem> sexos = new List<SelectListItem> {

				new SelectListItem { Value = "M", Text = "Masculino" },
				new SelectListItem { Value = "F", Text = "Feminino" }

			};
			List<SelectListItem> estados = new List<SelectListItem> {

				new SelectListItem { Value = "AC", Text = "Acre" },
				new SelectListItem { Value = "AL", Text = "Alagoas" },
				new SelectListItem { Value = "AP", Text = "Amapá" },
				new SelectListItem { Value = "AM", Text = "Amazonas" },
				new SelectListItem { Value = "BA", Text = "Bahia" },
				new SelectListItem { Value = "CE", Text = "Ceará" },
				new SelectListItem { Value = "DF", Text = "Distrito Federal" },
				new SelectListItem { Value = "ES", Text = "Espírito Santo" },
				new SelectListItem { Value = "GO", Text = "Goiás" },
				new SelectListItem { Value = "MA", Text = "Maranhão" },
				new SelectListItem { Value = "MT", Text = "Mato Grosso" },
				new SelectListItem { Value = "MS", Text = "Mato Grosso do Sul" },
				new SelectListItem { Value = "MG", Text = "Minas Gerais" },
				new SelectListItem { Value = "PA", Text = "Pará" },
				new SelectListItem { Value = "PB", Text = "Paraíba" },
				new SelectListItem { Value = "PR", Text = "Paraná" },
				new SelectListItem { Value = "PE", Text = "Pernambuco" },
				new SelectListItem { Value = "PI", Text = "Piauí" },
				new SelectListItem { Value = "RJ", Text = "Rio de Janeiro" },
				new SelectListItem { Value = "RN", Text = "Rio Grande do Norte" },
				new SelectListItem { Value = "RS", Text = "Rio Grande do Sul" },
				new SelectListItem { Value = "RO", Text = "Rondônia" },
				new SelectListItem { Value = "RR", Text = "Roraima" },
				new SelectListItem { Value = "SC", Text = "Santa Catarina" },
				new SelectListItem { Value = "SP", Text = "São Paulo" },
				new SelectListItem { Value = "SE", Text = "Sergipe" },
				new SelectListItem { Value = "TO", Text = "Tocantins" }
			};

			ViewBag.Estado = new SelectList(estados, "Value", "Text");
			ViewBag.Sexo = new SelectList(sexos, "Value", "Text");
			return View(pais);
		}

		[HttpPost]
		[Authorize(Roles = "Master, Admin")]
		[ValidateAntiForgeryToken]	
		public IActionResult UpdatePais(MdlPais pais)
		{

			if (ModelState.IsValid) {

				_paisRepository.UpdatePais(pais);
				return Redirect("/Pais/ListPais");
			}
			else
			{
				ModelState.AddModelError("", "Erro");
				return View(pais);
			}

		}

		[HttpPost]
		[Authorize(Roles = "Master, Admin")]
		[ValidateAntiForgeryToken]
		public IActionResult RemovePais(int id)
		{
			_paisRepository.DeletePais(id);
			return Redirect("/Pais/ListPais");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View(StatusCode(404));
		}
	}
}
