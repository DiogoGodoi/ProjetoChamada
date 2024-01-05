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
		public void CreateProfessorTurma(MdlProfessorTurma professorTurma)
		{
			_context.Professor_Turma.Add(professorTurma);
			_context.SaveChanges();	
		}
		public IEnumerable<MdlProfessorTurma> ListProfessoresTurmas()
		{
			var professoresTurmas = _context.Professor_Turma.Include(i => i.Turma)
															.Include(i => i.Turma.Escola)
															.ToList();
			return professoresTurmas;
		}
		public IEnumerable<MdlProfessorTurma> GetProfessoresTurmasId(int id)
		{
			var professoresTurmas = _context.Professor_Turma.Where(i => i.Professor.Id == id)
															.Include(i => i.Professor)
															.Include(i => i.Turma)
															.Include(i => i.Turma.Escola).ToList();
			return professoresTurmas;
		}
		public void DeleteProfessorTurma(int id)
		{
			var professorTurma = _context.Professor_Turma.FirstOrDefault(i => i.Fk_Professor_Id == id);

			_context.Professor_Turma.Remove(professorTurma);

			_context.SaveChanges();

		}
	}
}
