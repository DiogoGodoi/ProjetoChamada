using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

namespace AuthTeste.Repository
{
	public class ProfessoresRepository: IProfessorRepository
	{
		private readonly MeuContextoChamada _context;
		public ProfessoresRepository(MeuContextoChamada _context)
		{
			this._context = _context;
		}
		public IEnumerable<MdlProfessor> GetProfessor()
		{
			var professores = _context.Professor.ToList();

			return professores;
			
		}
		public MdlProfessor GetProfessorId(int id)
		{
			var professor = _context.Professor.FirstOrDefault(i => i.Id == id);

			return professor;

		}
		public void CreateProfessor(MdlProfessor professor)
		{
			_context.Professor.Add(professor);
			_context.SaveChanges();
		}
		public void UpdateProfessor(MdlProfessor professor)
		{
			var _professor = _context.Professor.FirstOrDefault(i => i.Id == professor.Id);

			if(_professor != null)
			{
				_professor.Nome = professor.Nome;
				_professor.Sobrenome = professor.Sobrenome;
				_professor.Cref = professor.Cref;

				_context.SaveChanges();
			}
		}
		public void RemoveProfessor(int id)
		{
			var professor = _context.Professor.FirstOrDefault(i => i.Id == id);
			_context.Professor.Remove(professor);	
			_context.SaveChanges();	
		}
	}
}
