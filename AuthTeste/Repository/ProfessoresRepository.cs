using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;

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
	}
}
