using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class ProfessorTurmaRepository : IProfessorTurmaRepository
	{
		private readonly MeuContextoChamada _context;
		public ProfessorTurmaRepository(MeuContextoChamada context)
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
		public ViewModelProfessorTurma GetProfessoresTurmasId(int id)
		{
			var professor = _context.Professor.FirstOrDefault(i => i.Id == id);

			var professoresTurmasId = _context.Professor_Turma.Where(i => i.Professor.Id == id)
															.Include(i => i.Turma)
															.Include(i => i.Turma.Escola).ToList();

			ViewModelProfessorTurma professorTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_professorTurmaList = professoresTurmasId
			};

			return professorTurma;
		}
		public void RemoverTurmaDoProfessor(MdlProfessorTurma professorTurma)
		{
			var _professorTurma = _context.Professor_Turma
				.FirstOrDefault(i => i.Fk_Professor_Id == professorTurma.Fk_Professor_Id && i.Fk_Turma_Id == professorTurma.Fk_Turma_Id);

			if (_professorTurma != null)
			{
				MdlProfessorTurma _mdlProfessorTurma = new MdlProfessorTurma();
				_mdlProfessorTurma = _professorTurma;
				_context.Professor_Turma.Remove(_mdlProfessorTurma);
				_context.SaveChanges();
			}
		}
		public void AdcionarTurmaAoProfessor(MdlProfessorTurma professorTurma)
		{
			var professor = _context.Professor_Turma
				.FirstOrDefault(i => i.Fk_Professor_Id == professorTurma.Fk_Professor_Id && i.Fk_Turma_Id == professorTurma.Fk_Turma_Id);

			if (professor == null)
			{
				_context.Professor_Turma.Add(professorTurma);
				_context.SaveChanges();
			}
		}
	}
}
