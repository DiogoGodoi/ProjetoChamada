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
		public async void CreatePais(MdlPais pais)
		{
			await _context.AddAsync(pais);
			await _context.SaveChangesAsync();	
		}
		public async void DeletePais(int id)
		{
			var pais = await _context.Pais.FirstOrDefaultAsync(i => i.Id == id);
			_context.Remove(pais);
			await _context.SaveChangesAsync();

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
		public async void UpdatePais(MdlPais pais)
		{
			var pai = await _context.Pais.FirstOrDefaultAsync(i => i.Id == pais.Id);

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

				await _context.SaveChangesAsync();
			}

		}
	}
}
