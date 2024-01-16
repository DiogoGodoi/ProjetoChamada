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
			try
			{
				var escolas = _contexto.Escola.ToList();

				if (escolas != null)
				{
					return escolas;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{

				throw new Exception("Erro interno" + ex.Message);

			}

		}
		public void InsertEscola(MdlEscola escola)
		{
			try
			{
				if (escola != null)
				{
					_contexto.Add(escola);
					_contexto.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro interno" + ex.Message);
			}

		}
		public MdlEscola GetEscolaId(int id)
		{
			try
			{

				var escola = _contexto.Escola.FirstOrDefault(i => i.Id == id);

				if (escola == null)
				{
					return null;
				}
				else
				{
					return escola;
				}
			}
			catch (Exception ex)
			{

				throw new Exception("Erro interno" + ex.Message);

			}
		}
		public void UpdateEscola(MdlEscola pmtEscola)
		{
			try
			{
				var escola = _contexto.Escola.FirstOrDefault(i => i.Id == pmtEscola.Id);

				if (escola != null)
				{
					escola.Nome = pmtEscola.Nome;
					escola.Logradouro = pmtEscola.Logradouro;
					escola.Numero = pmtEscola.Numero;
					escola.Bairro = pmtEscola.Bairro;
					escola.Cidade = pmtEscola.Cidade;
					escola.Estado = pmtEscola.Estado;
					escola.Contato = pmtEscola.Contato;
					escola.UrlImage = pmtEscola.UrlImage;

					_contexto.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro interno" + ex.Message);
			}
		}
		public bool RemoveEscola(int id)
		{
			try
			{
				var escola = _contexto.Escola.FirstOrDefault(i => i.Id == id);

				if (escola != null)
				{
					_contexto.Remove(escola);
					_contexto.SaveChanges();
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro interno" + ex.Message);
			}

		}
	}
}
