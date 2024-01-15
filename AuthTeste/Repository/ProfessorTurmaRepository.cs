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
			try
			{
				if (professorTurma != null)
				{
					_context.Professor_Turma.Add(professorTurma);
					_context.SaveChanges();
				}

			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public IEnumerable<MdlProfessorTurma> ListProfessoresTurmas()
		{
			try
			{
				var professoresTurmas = _context.Professor_Turma.Include(i => i.Turma)
												.Include(i => i.Turma.Escola)
												.ToList();
				if (professoresTurmas != null)
				{
					return professoresTurmas;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public ViewModelProfessorTurma GetProfessoresTurmasId(int id)
		{
			try
			{
				var professor = _context.Professor.FirstOrDefault(i => i.Id == id);

				var professoresTurmasId = _context.Professor_Turma.Where(i => i.Professor.Id == id)
																.Include(i => i.Turma)
																.Include(i => i.Turma.Escola).ToList();

				if (professor != null && professoresTurmasId != null)
				{
					ViewModelProfessorTurma professorTurma = new ViewModelProfessorTurma
					{
						_mdlProfessor = professor,
						_professorTurmaList = professoresTurmasId
					};

					return professorTurma;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}

		}
		public ViewModelProfessorTurma GetTurmaProfessoresId(int id)
		{
			try
			{
				var turma = _context.Turma.Include(i => i.Escola).FirstOrDefault(i => i.Id == id);

				var turmaProfessoresId = _context.Professor_Turma.Where(i => i.Turma.Id == id)
																.Include(i => i.Turma.Escola)
																.Include(i => i.Professor).ToList();

				if (turma != null && turmaProfessoresId != null)
				{
					ViewModelProfessorTurma turmaProfessor = new ViewModelProfessorTurma
					{
						_mdlTurma = turma,
						_professorTurmaList = turmaProfessoresId
					};

					return turmaProfessor;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public void RemoverTurmaDoProfessor(MdlProfessorTurma professorTurma)
		{
			try
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
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public void AdcionarTurmaAoProfessor(MdlProfessorTurma professorTurma)
		{
			try
			{
				var professor = _context.Professor_Turma
				.FirstOrDefault(i => i.Fk_Professor_Id == professorTurma.Fk_Professor_Id && i.Fk_Turma_Id == professorTurma.Fk_Turma_Id);

				if (professor == null)
				{
					_context.Professor_Turma.Add(professorTurma);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
	}
}
