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
		public IEnumerable<MdlEscola> Escolas => _contexto.Escola.ToList();
		public void InsertEscola(MdlEscola escola)
		{
			_contexto.Add(escola);
			_contexto.SaveChanges();
		}
		public MdlEscola GetEscolaId(int id)
		{
			var escola = _contexto.Escola.FirstOrDefault(i => i.Id == id);

			if(escola == null)
			{
				return null;
			}
			else
			{
				return escola;
			}
		}
		public void RemoveEscola(int id)
		{
			var escola = _contexto.Escola.FirstOrDefault(i => i.Id == id);
			if(escola != null)
			{
				_contexto.Remove(escola);
				_contexto.SaveChanges();
			}

		}
	}
}
