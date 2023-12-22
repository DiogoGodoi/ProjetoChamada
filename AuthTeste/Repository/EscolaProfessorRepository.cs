using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class EscolaProfessorRepository: IEscolaProfessorRepository
	{

		private readonly MeuContexto _context;

		public EscolaProfessorRepository(MeuContexto _context) {
		
		this._context = _context;	

		}
		public IEnumerable<MdlEscolaProfessor> EscolaProfessores => _context.Escola_Professor;

	}
}
