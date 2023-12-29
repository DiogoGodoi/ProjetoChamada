using AuthTeste.Models;
using AuthTeste.ViewModels;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorRepository
	{
		IEnumerable<MdlProfessor> GetProfessor();
		ViewModelProfessorTurma GetProfessorId(int id);
		void CreateProfessor(MdlProfessor professor);
	}
}
