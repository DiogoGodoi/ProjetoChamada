using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class ProfessorTurmaRepository: IProfessorTurmaRepository
	{
		private readonly MeuContexto _context;

		public ProfessorTurmaRepository(MeuContexto _context)
		{
			this._context = _context;
		}

		public IEnumerable<MdlProfessorTurma> ProfessorTurmas => _context.Professor_Turma;
	}
}
