using AuthTeste.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthTeste.Controllers
{
    [Authorize]
	public class HomeController : Controller
    {

        [Authorize]
        public IActionResult Menu()
        {
            ViewBag.CaminhoImg = "/css/images/home.png";
            ViewBag.TitleJumbotron = "HOME";

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
