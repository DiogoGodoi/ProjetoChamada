using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class EscolasRepository : IEscolasRepository
	{
		private readonly MeuContexto _contexto;

		public EscolasRepository(MeuContexto _contexto)
		{
			this._contexto = _contexto;
		}
		public IEnumerable<MdlEscola> Escolas => _contexto.Escola;

		}
}
