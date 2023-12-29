using AuthTeste.Contexto;
using AuthTeste.Models;
using AuthTeste.Repository.Interfaces;
using AuthTeste.ViewModels;

namespace AuthTeste.Repository
{
	public class ProfessoresRepository: IProfessorRepository
	{
		private readonly MeuContexto _context;
		private readonly ProfessorTurmaRepository _professorTurmaRepository;
		public ProfessoresRepository(MeuContexto _context, ProfessorTurmaRepository _professorTurmaRepository)
		{
			this._context = _context;
			this._professorTurmaRepository = _professorTurmaRepository;
		}
		public IEnumerable<MdlProfessor> GetProfessor()
		{
			var professores = _context.Professor.ToList();

			return professores;
			
		}
		public ViewModelProfessorTurma GetProfessorId(int id)
		{
			var professor = _context.Professor.FirstOrDefault(i => i.Id == id);

			var professorTurma = _professorTurmaRepository.GetProfessoresTurmasId(id);

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
