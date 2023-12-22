using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public class IProfessorTurmaRepository
	{
		IEnumerable<MdlProfessorTurma> ProfessorTurmas  { get; }
	}
}
