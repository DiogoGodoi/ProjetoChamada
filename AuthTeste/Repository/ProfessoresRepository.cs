using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class ProfessoresRepository : IProfessorRepository
	{
		private readonly MeuContextoChamada _context;
		public ProfessoresRepository(MeuContextoChamada _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlProfessor> GetProfessor()
		{
			try
			{
				var professores = _context.Professor.ToList();

				if (professores.Count > 0)
				{
					return professores;
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
		public MdlProfessor GetProfessorId(int id)
		{
			try
			{
				var professor = _context.Professor.FirstOrDefault(i => i.Id == id);

				if (professor != null)
				{
					return professor;
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
		public void CreateProfessor(MdlProfessor professor)
		{
			try
			{
				if (professor != null)
				{
					_context.Professor.Add(professor);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public void UpdateProfessor(MdlProfessor professor)
		{
			try
			{
				var _professor = _context.Professor.FirstOrDefault(i => i.Id == professor.Id);

				if (_professor != null)
				{
					_professor.Nome = professor.Nome;
					_professor.Sobrenome = professor.Sobrenome;
					_professor.Cref = professor.Cref;

					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Erro" + ex.Message);
			}
		}
		public void RemoveProfessor(int id)
		{
			try
			{
				var professor = _context.Professor.FirstOrDefault(i => i.Id == id);
				if (professor != null)
				{
					_context.Professor.Remove(professor);
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
