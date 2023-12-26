using Microsoft.AspNetCore.Mvc;

namespace AuthTeste.Components
{
	public class UsuarioSessao: ViewComponent
	{
		public string usuario { get; set; } = "";
		public IViewComponentResult Invoke()
		{
			var usuarioSessao = HttpContext.Session.GetString("UsuarioEmail") ?? "";

            UsuarioSessao sessao = new UsuarioSessao
            {
			usuario = usuarioSessao
            };

			return View(sessao);
		}

	}
}
