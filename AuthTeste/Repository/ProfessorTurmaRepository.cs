using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class ProfessorTurmaRepository: IProfessorTurmaRepository
	{
		private readonly MeuContexto _context;
		public ProfessorTurmaRepository(MeuContexto context)
		{
			this._context = context;
		}
		public IEnumerable<MdlProfessorTurma> ListProfessoresTurmas()
		{
			var professoresTurmas = _context.Professor_Turma.ToList();

			return professoresTurmas;
		}
		public IEnumerable<MdlProfessorTurma> GetProfessoresTurmasId(int id)
		{
			var professoresTurmas = _context.Professor_Turma.Where(i => i.Professor.Id == id)
															.Include(i => i.Turma)
															.Include(i => i.Turma.Escola).ToList();
			return professoresTurmas;
		}
	}
}
