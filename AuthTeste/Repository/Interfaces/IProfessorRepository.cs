using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public class IProfessorRepository
	{
		IEnumerable<MdlProfessor> Professores { get; }
	}
}
