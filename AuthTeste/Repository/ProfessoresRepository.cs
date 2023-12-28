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
		public ViewModelProfessoresEscolasTurmas GetProfessores()
		{

			ViewModelProfessoresEscolasTurmas _professorEscolaTurma = new ViewModelProfessoresEscolasTurmas
			{
				_mdlEscolaProfessorList = _context.Escola_Professor.Include(i => i.Professor)
											.Include(i => i.Escola)
											.OrderBy(i => i.Professor.Nome)
											.ToList(),
				_mdlProfessorTurmaList = _context.Professor_Turma.Include(i => i.Turma)
											.OrderBy(i => i.Professor.Nome)
											.ToList(),
			};

			return _professorEscolaTurma;
			
		}
		public ViewModelProfessoresEscolasTurmas GetProfessorId(int id)
		{
			var professorEscola = _context.Escola_Professor.Include(i => i.Professor)
													 .Include(i => i.Escola)
													 .FirstOrDefault(i => i.Professor.Id == id);
			
			var professorTurma = _context.Professor_Turma.Include(i => i.Professor)
														 .Include(i => i.Turma)
														 .FirstOrDefault(i => i.Professor.Id == id);

			ViewModelProfessoresEscolasTurmas _mdlProfessorEscolaTurma = new ViewModelProfessoresEscolasTurmas
			{
				_mdlEscolaProfessor = professorEscola,
				_mdlProfessorTurma = professorTurma
			};

			return _mdlProfessorEscolaTurma;

		}
		public void CreateProfessor(ViewModelProfessoresEscolasTurmas professor)
		{
			if(professor._professor != null 
			  && professor._mdlEscolaProfessor != null 
			  && professor._mdlProfessorTurma != null) 
			{
			_context.Professor.Add(professor._professor);
			_context.Escola_Professor.Add(professor._mdlEscolaProfessor);
			_context.Professor_Turma.Add(professor._mdlProfessorTurma);
			_context.SaveChanges();
			}
		}
	}
}
