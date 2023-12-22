using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public class IEscolaProfessorRepository
	{
		IEnumerable<MdlEscolaProfessor> EscolaProfessores { get; }
	}
}
