using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class PaisRepository : IPaisRepository
	{
		private readonly MeuContextoChamada _context;
		public PaisRepository(MeuContextoChamada _context)
		{
			this._context = _context;
		}
		public void CreatePais(MdlPais pais)
		{
			try
			{
				if (pais != null)
				{
					_context.Add(pais);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro interno" + ex.Message);
			}
		}
		public void DeletePais(int id)
		{
			try
			{
				var pais = _context.Pais.FirstOrDefault(i => i.Id == id);

				if (pais != null)
				{
					_context.Remove(pais);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro interno" + ex.Message);
			}
		}
		public MdlPais GetPaisId(int id)
		{
			try
			{
				var pais = _context.Pais.FirstOrDefault(i => i.Id == id);

				if (pais != null)
				{
					return pais;
				}
				else
				{
					return null;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public IEnumerable<MdlPais> ListPais()
		{
			try
			{
				var pais = _context.Pais.ToList();

				if (pais.Count() > 0)
				{
					return pais;
				}
				else
				{
					return null;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}
		public void UpdatePais(MdlPais pais)
		{
			try
			{
				var pai = _context.Pais.FirstOrDefault(i => i.Id == pais.Id);

				if (pai != null)
				{
					pai.Nome = pais.Nome;
					pai.Sobrenome = pais.Sobrenome;
					pai.Cpf = pais.Cpf;
					pai.Sexo = pais.Sexo;
					pai.Contato = pais.Contato;
					pai.Logradouro = pais.Logradouro;
					pai.Numero = pais.Numero;
					pai.Logradouro = pais.Logradouro;
					pai.Cidade = pais.Cidade;
					pai.Estado = pais.Estado;

					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}
	}
}
