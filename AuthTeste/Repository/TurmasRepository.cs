using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class TurmasRepository: ITurmaRepository
	{
		private readonly MeuContexto _context;

		public TurmasRepository(MeuContexto _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlTurma> Turmas => _context.Turma;
	}
}
