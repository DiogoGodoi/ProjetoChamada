using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AuthTeste.Repository
{
	public class ProfessoresRepository: IProfessorRepository
	{
		private readonly MeuContexto _context;
		public ProfessoresRepository(MeuContexto _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlProfessor> GetProfessor()
		{
			var professores = _context.Professor.ToList();

			return professores;
			
		}
		public ViewModelProfessorTurma GetProfessorId(int id)
		{
			var professor = _context.Professor.FirstOrDefault(i => i.Id == id);

			var professorTurma = _context.Professor_Turma.Where(i => i.Professor.Id == id)
														 .Include(i => i.Turma)
														 .Include(i => i.Turma.Escola)
														 .ToList();

			ViewModelProfessorTurma _mdlProfessorEscolaTurma = new ViewModelProfessorTurma
			{
				_mdlProfessor = professor,
				_mdlProfessorTurmaList = professorTurma
			};

			return _mdlProfessorEscolaTurma;

		}
		public void CreateProfessor(MdlProfessor professor)
		{
			_context.Professor.Add(professor);
			_context.SaveChanges();
		}
	}
}
