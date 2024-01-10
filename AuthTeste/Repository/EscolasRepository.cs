using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class EscolasRepository : IEscolasRepository
	{
		private readonly MeuContextoChamada _contexto;
		public EscolasRepository(MeuContextoChamada _contexto)
		{
			this._contexto = _contexto;
		}
		public IEnumerable<MdlEscola> GetEscolas()
		{
			return _contexto.Escola.ToList();
		}  
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
		public void UpdateEscola(MdlEscola pmtEscola)
		{
			var escola = _contexto.Escola.FirstOrDefault(i => i.Id == pmtEscola.Id);

			if(escola != null)
			{
				escola.Nome = pmtEscola.Nome;
				escola.Logradouro = pmtEscola.Logradouro;
				escola.Numero = pmtEscola.Numero;
				escola.Bairro = pmtEscola.Bairro;
				escola.Cidade = pmtEscola.Cidade;
				escola.Estado = pmtEscola.Estado;

				_contexto.SaveChanges();	
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
