using AuthTeste.Models;
using AuthTeste.ViewModels;

namespace AuthTeste.Repository.Interfaces
{
	public interface IProfessorTurmaRepository
	{
		void CreateProfessorTurma(MdlProfessorTurma professorTurma);
		IEnumerable<MdlProfessorTurma> ListProfessoresTurmas();
		ViewModelProfessorTurma GetProfessoresTurmasId(int id);
		void AdcionarTurmaAoProfessor(MdlProfessorTurma professorTurma);
		void RemoverTurmaDoProfessor(MdlProfessorTurma professorTurma);
	}
}
