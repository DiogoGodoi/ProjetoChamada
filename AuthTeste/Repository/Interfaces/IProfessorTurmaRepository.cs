using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorTurmaRepository
	{
		void CreateProfessorTurma(MdlProfessorTurma professorTurma);
		IEnumerable<MdlProfessorTurma> ListProfessoresTurmas();
		IEnumerable<MdlProfessorTurma> GetProfessoresTurmasId(int id);
	}
}
