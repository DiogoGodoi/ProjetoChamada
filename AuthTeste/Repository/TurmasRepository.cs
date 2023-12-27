using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class TurmasRepository: ITurmaRepository
	{
		private readonly MeuContexto _context;

		public TurmasRepository(MeuContexto _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlTurma> Turmas()
		{
			var turmas = _context.Turma.Include(i => i.Escola).ToList();

			return turmas;
		}
		public void CreateTurma(MdlTurma turma) {

			if(turma != null)
			{
				_context.Add(turma);
				_context.SaveChanges();
			}

		}
	}
}
