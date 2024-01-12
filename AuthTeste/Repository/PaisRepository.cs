using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class PaisRepository: IPaisRepository
	{
		private readonly MeuContextoChamada _context;
		public PaisRepository(MeuContextoChamada _context)
		{
			this._context = _context;	
		}
		public void CreatePais(MdlPais pais)
		{
			_context.Add(pais);
			_context.SaveChanges();	
		}
		public void DeletePais(int id)
		{
			var pais = _context.Pais.FirstOrDefault(i => i.Id == id);
			_context.Remove(pais);
		    _context.SaveChanges();

		}
		public MdlPais GetPaisId(int id)
		{
			var pais = _context.Pais.FirstOrDefault(i => i.Id == id);

			return pais;

		}
		public IEnumerable<MdlPais> ListPais()
		{
			var pais = _context.Pais.ToList();
			return pais;
		}
		public void UpdatePais(MdlPais pais)
		{
			var pai = _context.Pais.FirstOrDefault(i => i.Id == pais.Id);

			if(pai != null)
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
	}
}
