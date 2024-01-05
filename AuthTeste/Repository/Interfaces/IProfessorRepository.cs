using AuthTeste.Models;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorRepository
	{
		IEnumerable<MdlProfessor> GetProfessor();
		MdlProfessor GetProfessorId(int id);
		void CreateProfessor(MdlProfessor professor);
		void UpdateProfessor(MdlProfessor professor);
		void RemoveProfessor(int id);
	}
}
