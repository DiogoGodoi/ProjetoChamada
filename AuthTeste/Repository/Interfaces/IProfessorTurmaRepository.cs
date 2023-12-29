using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorTurmaRepository
	{
		IEnumerable<MdlProfessorTurma> ListProfessoresTurmas();
		IEnumerable<MdlProfessorTurma> GetProfessoresTurmasId(int id);
	}
}
